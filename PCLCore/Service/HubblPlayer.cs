using System;
using System.Collections.Generic;
using System.Linq;
using Hubl.Core.Model;
using System.Threading.Tasks;
using System.Threading;

namespace Hubl.Core.Service
{
	public class HubblPlayer : IMusicPlayer
	{
		private IMusicPlayerBackend _backend;
		private Task _playerTask;
		private readonly CancellationTokenSource _cancellationTokenSource;

		public HubblPlayer (IMusicPlayerBackend backend)
		{
			_backend = backend;
			Playlist = new List<PlaylistEntry> ();
		   _cancellationTokenSource = new CancellationTokenSource ();
		}


		#region IMusicPlayer implementation
		PlaylistEntry IMusicPlayer.QueueTrack (User user, Track track)
		{
			var entry = new PlaylistEntry () {User = user, Track = track};
			Playlist.Enqueue (entry);
			return entry;
		}

		//TODO: ask my c sharp guru about methods to make it immutable
		//TODO: private set
		public PlaylistEntry CurrentPlayedEntry { get; private set; }
		public IEnumerable<PlaylistEntry> Playlist { get; private set; }

		Track IMusicPlayer.GetTrackInfo (string path)
		{
			return _backend.GetTrackInfo (path);
		}

		void IMusicPlayer.Play ()
		{
			var cancelTk = _cancellationTokenSource.Token;
			_playerTask = Task.Run (() => {
				if (cancelTk.IsCancellationRequested)
					cancelTk.ThrowIfCancellationRequested (); // i copypaste string from msdn. Is `if' rly requered when the method called `throwIf..' /0

				while (!(cancelTk.IsCancellationRequested || CurrentPlayedEntry == null && Playlist.Count == 0))
				{
					var track = _backend.CurrentPlayedTrack;
					//TODO: maybe we should store played tracks somewhere to reuse. Anyway, now KILL IT WITH FIRE
					if (track == null) CurrentPlayedEntry = null;
					if (track == null && Playlist.Count > 0)
					{
						CurrentPlayedEntry = Playlist.Dequeue ();
						_backend.PlayTrack (CurrentPlayedEntry.Track);
					}
					Task.Delay(100).RunSynchronously();
				}
				CurrentPlayedEntry = null;
				_backend.PlayTrack (null);
			}, cancelTk);
		}

		void IMusicPlayer.Stop ()
		{
			_cancellationTokenSource.Cancel ();
			_playerTask = null;
		}
		#endregion
	}
}

