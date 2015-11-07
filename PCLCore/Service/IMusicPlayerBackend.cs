using System;
using Hubl.Core.Model;

namespace Hubl.Core.Service
{
	public interface IMusicPlayerBackend
	{
		Track GetTrackInfo (string path);

		void PlayTrack (Track track);

		void PauseCurrentTrack ();

		Track CurrentPlayedTrack { get; }
	}
}

