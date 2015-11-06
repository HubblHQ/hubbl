using System;
using System.Collections.Generic;

namespace Hubl.Mobile
{
	public class SongsPickerEventArgs : EventArgs 
	{
		public List<Song> PickedSongs {get; set; }
		public SongsPickerEventArgs()
		{
			PickedSongs = new List<Song> ();
		}
	}
	public interface ISongsPicker
	{
		event EventHandler<SongsPickerEventArgs> SongsPicked;
		void StartPicking ();
	}
}

