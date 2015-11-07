using Hubl.Core.Model;
using System.Collections.Generic;

namespace Hubl.Core.Service
{
	public interface IMusicPlayer
    {
		IMusicPlayer (IMusicPlayerBackend backend);

		PlaylistEntry QueueTrack (User user, Track track);

		// rules of sorting by priority are not applied to this track, i think it should be given separately from the playlist
		PlaylistEntry CurrentPlayedEntry { get;  } 

		//TODO: queue??
		IEnumerable<PlaylistEntry> Playlist { get;  } 

		Track GetTrackInfo (string path);

		void Play ();
		void Stop ();
		//TODO: what a shame, player can't pause!
		// void Pause ();
    }
}
