﻿using System.Collections.Generic;
using Hubbl.Core.Model;

namespace Hubbl.Core.Service
{
	public interface IMusicPlayer
    {

		PlaylistEntry QueueTrack (HubblUser user, Track track);

		PlaylistEntry CurrentPlayedEntry { get; }
		List<PlaylistEntry> Playlist { get; }

		void LikeTrack (HubblUser user, int entryId);
		void DislikeTrack (HubblUser user, int entryId);
		

		void Play ();
		void Stop ();
		//TODO: what a shame, player can't pause!
		// void Pause ();
    }
}
