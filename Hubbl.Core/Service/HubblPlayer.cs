﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hubbl.Core.Model;

namespace Hubbl.Core.Service
{
	internal class UserActivitiesRecord
	{
		public double basePriority;
		public int activitiesCount;
		public DateTime lastActivityTime;
	}

	public class HubblPlayer : IMusicPlayer
	{
		private const int AVG_TRACK_LENGTH_MS = 300000;

		private readonly IMusicPlayerBackend _backend;
		private Task _playerTask;
		private CancellationTokenSource _cancellationTokenSource;

		private readonly Dictionary<string, UserActivitiesRecord> _usersActivities;
		private readonly Dictionary<int, List<string>> _usersMarkedByEntryId;
		private readonly List<PlaylistEntry> _playlistEntries;
		private int _activeUsersCount;
		private int _freeEntryId;

		private bool _playing;

		public HubblPlayer(IMusicPlayerBackend backend)
		{
			_backend = backend;
			Playlist = new List<PlaylistEntry>();
			_cancellationTokenSource = new CancellationTokenSource();

			_usersActivities = new Dictionary<string, UserActivitiesRecord>();
			_usersMarkedByEntryId = new Dictionary<int, List<string>>();
			_playlistEntries = new List<PlaylistEntry>();
			_activeUsersCount = 0;
			_freeEntryId = 0;

			_playing = false;
		}

		#region IMusicPlayer implementation

		public PlaylistEntry QueueTrack(HubblUser user, Track track)
		{
			UpdateUserPriorities();

			if (!_usersActivities.ContainsKey(user.Id))
			{
				var newActivity = new UserActivitiesRecord
				{
					basePriority = 1.0,
					activitiesCount = 0,
					lastActivityTime = DateTime.Now
				};
				_usersActivities.Add(user.Id, newActivity);
			}
			var activitiesRecord = _usersActivities[user.Id];

			if (activitiesRecord.activitiesCount++ == 0)
				++_activeUsersCount;

			var entry = new PlaylistEntry
			{
				User = user,
				Track = track,
				Id = _freeEntryId++,
				LikesNum = 0,
				DislikesNum = 0,
				Priority = activitiesRecord.basePriority
			};
			activitiesRecord.basePriority *= Math.Pow(0.5, track.Duration.TotalMilliseconds/AVG_TRACK_LENGTH_MS);
			_usersMarkedByEntryId.Add(entry.Id, new List<string>());
			_usersMarkedByEntryId[entry.Id].Add(user.Id);

			Playlist.Add(entry);
			Playlist.Sort(comparePlaylistEntries);

			_playlistEntries.Add(entry);

			if (!_playing)
				Play();

			return entry;
		}

		private void UpdateUserPriorities()
		{
			var now = DateTime.Now;
			foreach (var activitiesRecordKV in _usersActivities)
			{
				var activitiesRecord = activitiesRecordKV.Value;
				if (_activeUsersCount > 0)
					activitiesRecord.basePriority =
						Math.Min(
							activitiesRecord.basePriority*
							Math.Pow(2,
								0.5*(now - activitiesRecord.lastActivityTime).TotalMilliseconds/(_activeUsersCount*AVG_TRACK_LENGTH_MS)), 1.0);
				activitiesRecord.lastActivityTime = now;
			}
		}

		private void NextTrack()
		{
			UpdateUserPriorities();
			if (CurrentPlayedEntry != null)
			{
				foreach (var userId in _usersMarkedByEntryId[CurrentPlayedEntry.Id])
				{
					var activitiesRecord = _usersActivities[userId];
					if (--activitiesRecord.activitiesCount == 0)
						--_activeUsersCount;
				}
				CurrentPlayedEntry = null;
			}
			if (Playlist.Count > 0)
			{
				CurrentPlayedEntry = Playlist[0];
				_backend.PlayTrack(CurrentPlayedEntry.Track);
				Playlist.RemoveAt(0);
			}
		}

		public void LikeTrack(HubblUser user, int entryId)
		{
			foreach (var userMarkedId in _usersMarkedByEntryId[entryId])
				if (userMarkedId == user.Id)
					return;
			_usersMarkedByEntryId[entryId].Add(user.Id);

			UpdateUserPriorities();
			if (!_usersActivities.ContainsKey(user.Id))
			{
				var newActivity = new UserActivitiesRecord
				{
					basePriority = 1.0,
					activitiesCount = 0,
					lastActivityTime = DateTime.Now
				};
				_usersActivities.Add(user.Id, newActivity);
			}
			var activitiesRecord = _usersActivities[user.Id];
			if (activitiesRecord.activitiesCount++ == 0)
				++_activeUsersCount;

			var entry = _playlistEntries[entryId];
			++entry.LikesNum;
			entry.Priority *= _activeUsersCount/(_activeUsersCount - 1.0);

			Playlist.Sort(comparePlaylistEntries);
		}

		public void DislikeTrack(HubblUser user, int entryId)
		{
			foreach (var userMarkedId in _usersMarkedByEntryId[entryId])
				if (userMarkedId == user.Id)
					return;
			_usersMarkedByEntryId[entryId].Add(user.Id);

			UpdateUserPriorities();
			if (!_usersActivities.ContainsKey(user.Id))
			{
				var newActivity = new UserActivitiesRecord
				{
					basePriority = 1.0,
					activitiesCount = 0,
					lastActivityTime = DateTime.Now
				};
				_usersActivities.Add(user.Id, newActivity);
			}
			var activitiesRecord = _usersActivities[user.Id];
			if (activitiesRecord.activitiesCount++ == 0)
				++_activeUsersCount;

			var entry = _playlistEntries[entryId];
			++entry.DislikesNum;
			entry.Priority *= (_activeUsersCount - 1.0)/_activeUsersCount;

			Playlist.Sort(comparePlaylistEntries);
		}

		private static int comparePlaylistEntries(PlaylistEntry e1, PlaylistEntry e2)
		{
			return Math.Abs(e1.Priority - e2.Priority) < 0.001
				? e2.Id - e1.Id
				: e2.Priority < e1.Priority ? -1 : 1;
		}


		//TODO: ask my c sharp guru about methods to make it immutable
		public PlaylistEntry CurrentPlayedEntry { get; private set; }
		public List<PlaylistEntry> Playlist { get; private set; }

		public void Play()
		{
			if (_playing)
				return;
			var cancelTk = _cancellationTokenSource.Token;
			UpdateUserPriorities();
			Task.Run(() =>
			{
				if (cancelTk.IsCancellationRequested)
					cancelTk.ThrowIfCancellationRequested();
				// i copypaste string from msdn. Is `if' rly requered when the method called `throwIf..' /0

				while (!(cancelTk.IsCancellationRequested || CurrentPlayedEntry == null && Playlist.Count == 0))
				{
					var track = _backend.CurrentPlayedTrack;
					if (track == null) NextTrack();
					// Threa.Sleep (100);
					Task.Delay(100).Wait();
				}
				CurrentPlayedEntry = null;
				_backend.PlayTrack(null);
				_playing = false;
			}, cancelTk);
		}

		void IMusicPlayer.Stop()
		{
			_cancellationTokenSource.Cancel();
			_playerTask = null;
			_cancellationTokenSource = new CancellationTokenSource();
		}

		#endregion
	}
}

