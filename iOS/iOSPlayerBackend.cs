using System;
using AVFoundation;
using System.Runtime.InteropServices;
using Hubbl.Core.Service;
using Hubbl.Core.Model;
using Foundation;
using CoreMedia;
using CoreText;
using UIKit;
using System.IO;
using PassKit;
using AudioToolbox;

[assembly: Xamarin.Forms.Dependency(typeof(Hubbl.Mobile.iOS.iOSPlayerBackend))]
namespace Hubbl.Mobile.iOS
{

	public class iOSPlayerBackend : IMusicPlayerBackend
	{
		MusicPlayer player;
		public iOSPlayerBackend()
		{
			
		}
		#region IMusicPlayerBackend implementation
		public Track GetTrackInfo (string path)
		{
			throw new NotImplementedException ();
		}
		public void PlayTrack (Track track)
		{
			var path = Path.Combine (NSBundle.MainBundle.ResourcePath, "testsound.mp3");
			var data = System.IO.File.ReadAllBytes (path);
			var url = NSUrl.FromFilename (path);
			var status = MusicPlayerStatus.Success;
			player = MusicPlayer.Create (out status);
			player.MusicSequence = new MusicSequence ();
			player.MusicSequence.LoadFile (url, MusicSequenceFileTypeID.Any);
			player.Start ();
			/*AppDelegate.Player = AVAudioPlayer.FromUrl (url);
			AppDelegate.Player.NumberOfLoops = 1;

			var res = AppDelegate.Player.PrepareToPlay ();
			//player.AddPeriodicTimeObserver (CoreMedia,CMTime.FromSeconds (1, 1), );

			var r = AppDelegate.Player.Play ();
			AppDelegate.Player.DecoderError += (sender, e) => {
				var a = 3;
			};
			AppDelegate.Player.FinishedPlaying += (sender, e) => {
				var a = 4;
			};
			AppDelegate.Player.BeginInterruption += (sender, e) => {
				var a = 5;
			};
			AppDelegate.Player.EndInterruption += (sender, e) => {
				var a = 6; 
			};
			*/
		}
		public void PauseCurrentTrack ()
		{
			var aa = 4;
		}
		public Track CurrentPlayedTrack {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
			
	}
}

