using Hubl.Core.Model;
using System.Collections.Generic;

namespace Hubl.Core.Service
{
	public interface IMusicPlayer
    {
		// IMusicPlayer (IMusicPlayerBackend backend);

		PlaylistEntry QueueTrack (User user, Track track);
		PlaylistEntry CurrentPlayedEntry { get;  } // rules of sorting by priority are not applied to this track, i think it should be given separately from the playlist
		Queue<PlaylistEntry> Playlist { get;  } //TODO: queue??
		Track GetTrackInfo (string path);

		void Play ();
		void Stop ();
		//TODO: what a shame, player can't pause!
		// void Pause ();
    }
}
