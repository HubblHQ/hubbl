//using System;
//using System.Threading.Tasks;
//using Hubbl.Core;
//using Xamarin.Auth;
//using Xamarin.Forms;
//
//namespace Hubbl.Mobile.iOS.ExternalModules
//{
//	public class Authorization
//	{
//		public Authorization (User _user)
//		{
//			this._user = _user;
//		}
//
//		private readonly User _user;
//
//		void LoginToVk ()
//		{
//			var auth = new OAuth2Authenticator (
//				SecKey: VkSettings.SecureKey,
//				clientId: VkSettings.AppID,
//				scope: "friends,video,audio",
//				authorizeUrl: new Uri ("https://oauth.vk.com/authorize"),
//				redirectUrl: new Uri ("https://oauth.vk.com/blank.html"));
//
//			auth.AllowCancel = true;
//
//			auth.Completed += (s, ee) => {
//				if (!ee.IsAuthenticated) {
//					//TODO: THINGS IF NOT AUTHORISED
//					return;
//				}
//				else
//				{
//					var token = ee.Account.Properties ["access_token"].ToString ();
//					var uid = ee.Account.Properties ["user_id"].ToString ();             
//					_user.VkUserInfo = new VkUserInfo(token, uid);
//				}
//			};
//			//TODO SOMETHING ELSE
//			var intent = auth.GetUI (this);
//		}
//
//		private static readonly TaskScheduler UIScheduler = TaskScheduler.FromCurrentSynchronizationContext();
//
//		protected override void OnCreate (Bundle bundle)
//		{
//			base.OnCreate (bundle);
//			SetContentView (Resource.Layout.Main);
//			var vk = FindViewById<Button> (Resource.Id.VkButton);           
//			vk.Click += delegate { LoginToVk();};
//		}
//
//	}
//}
//
