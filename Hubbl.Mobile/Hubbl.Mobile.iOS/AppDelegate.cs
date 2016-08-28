using AVFoundation;
using FormsToolkit.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Hubbl.Mobile.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
	{
		public static AVAudioPlayer Player;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Forms.Init();
			Toolkit.Init();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}

