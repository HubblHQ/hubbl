
using System;

using Foundation;
using UIKit;
using Hubbl.Core;
using Hubbl.Core;
using Xamarin.Auth;
using Autofac;
using Hubbl.Core.Service;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Hubbl.Mobile;
using Hubbl.Mobile.iOS;
using System.Runtime.InteropServices;

[assembly:ExportRenderer(typeof(AuthPage), typeof(AuthController))]
namespace Hubbl.Mobile.iOS
{
	public partial class AuthController : PageRenderer
	{

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		AuthPage page;
		UIViewController controller;
		void LoginToVk ()
		{
			var auth = new OAuth2Authenticator (
				clientId: VkSettings.AppID,
				scope: "friends,video,audio",
				//clientSecret: VkSettings.SecureKey,
				authorizeUrl: new Uri ("https://oauth.vk.com/authorize"),
				redirectUrl: new Uri ("https://oauth.vk.com/blank.html"));
			auth.AllowCancel = true;
			auth.Completed += (s, ee) => {
				if (!ee.IsAuthenticated) {					
					return;
				}
				else
				{
					var token = ee.Account.Properties ["access_token"].ToString ();
					var userId = ee.Account.Properties ["user_id"].ToString ();		
					var user = App.Container.Resolve<ISession> ().CurrentUser;
					user.VkUserInfo = new VkUserInfo(token, userId);
					auth.OnSucceeded (ee.Account);
					user.Save ();
					//this.DismissViewController (true, null);
					page.Navigation.PopAsync ();
				}
			};
			auth.Error += (sender, e) => {
				var a = 4;
			};

			var nav = auth.GetUI () as UINavigationController;
			controller = nav.TopViewController;
			this.AddChildViewController (controller);
			this.View.AddSubview (controller.View);
		}


		public override void ViewDidLoad ()
		{
			
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{			
			var user = App.Container.Resolve<ISession> ().CurrentUser;
			if (user.VkUserInfo == null) {
				LoginToVk ();
			}
			else {
				page.Navigation.PopModalAsync ();
			}
			base.ViewWillAppear (animated);
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			page = e.NewElement as AuthPage;
		}
	}
}

