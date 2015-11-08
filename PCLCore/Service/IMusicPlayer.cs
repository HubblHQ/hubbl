using Hubl.Core.Model;
using System.Collections.Generic;

namespace Hubl.Core.Service
{
	public interface IMusicPlayer
    {

		PlaylistEntry QueueTrack (User user, Track track);

		PlaylistEntry CurrentPlayedEntry { get; }

		List<PlaylistEntry> Playlist { get; }

		void Play ();
		void Stop ();
		//TODO: what a shame, player can't pause!
		// void Pause ();
    }
}
