using System;
using Hubbl.Core.Model;

namespace Hubbl.Core.Service
{
	public interface IMusicPlayerBackend
	{
		Track GetTrackInfo (string path);

		void PlayTrack (Track track);

		void PauseCurrentTrack ();

		Track CurrentPlayedTrack { get; }
	}
}

