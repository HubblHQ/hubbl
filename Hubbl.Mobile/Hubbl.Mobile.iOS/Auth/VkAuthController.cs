using System;
using System.Diagnostics;
using Hubbl.Core;
using Hubbl.Core.Service;
using Hubbl.Mobile.iOS.Auth;
using Hubbl.Mobile.Pages;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(VkAuthPage), typeof(VkAuthController))]
namespace Hubbl.Mobile.iOS.Auth
{
	public class VkAuthController : PageRenderer
	{
		private VkAuthPage page;
		private UIViewController controller;

		private void LoginToVk ()
		{
			var auth = new OAuth2Authenticator(
				clientId: VkSettings.AppID,
				scope: "friends,video,audio",
				//clientSecret: VkSettings.SecureKey,
				authorizeUrl: new Uri("https://oauth.vk.com/authorize"),
				redirectUrl: new Uri("https://oauth.vk.com/blank.html")) {AllowCancel = true};
			auth.Completed += (s, ee) => {
				if (!ee.IsAuthenticated) {
					#if DEBUG
						Debug.WriteLine("Not authenticated! Try to relogin");
						throw new AuthException("Not authenticated! In VkAuthController");
					#endif
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
			auth.Error += (sender, e) =>
			{
				throw e.Exception;
			};

			var nav = auth.GetUI () as UINavigationController;
			Debug.Assert(nav != null, "nav != null");
			controller = nav.TopViewController;
			AddChildViewController (controller);
			View.AddSubview (controller.View);
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
			page = e.NewElement as VkAuthPage;
		}
	}
}

