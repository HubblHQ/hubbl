using System;
using Android.Provider;
using Android.Content;
using Xamarin.Forms;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(Hubl.Mobile.Droid.SongsPicker_Droid))]
namespace Hubl.Mobile.Droid
{
	public class SongsPicker_Droid : ISongsPicker
	{
		public static List<Song> Songs { get; set; }
		public SongsPicker_Droid ()
		{
		}

		public event EventHandler<SongsPickerEventArgs> SongsPicked;

		public void StartPicking()
		{
			var ctx = Forms.Context;
			ctx.StartActivity (typeof(SongsPickingActivity));
		}

		protected void OnSongsPicked (SongsPickerEventArgs e) 
		{
			
		}

	}
}

