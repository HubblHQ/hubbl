using System;
using Hubbl.Core.Model;

namespace Hubbl.Core.Service
{
	public interface IMusicPlayerBackend
	{
		Track GetTrackInfo(string path);

		void PlayTrack(Track track);

		void PauseCurrentTrack();

	    void ResumeCurrentTrack();

	    void ChangeVolume(int volume);

		Track CurrentPlayedTrack { get; }
	}
}

