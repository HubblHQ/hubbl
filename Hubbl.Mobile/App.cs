using FreshMvvm;
using Hubbl.Mobile.PageModels;
using Hubbl.Mobile.PageModels.Vkontakte;
using Hubbl.Mobile.Services;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Xamarin.Forms;

namespace Hubbl.Mobile
{
	public class App : Application
	{
		private static IFreshIOC Container { get; set; }
		
		public static INetworkMessageRouter Router => Container.Resolve<INetworkMessageRouter>();


		public App()
		{
			Container = FreshIOC.Container;
			Container.Register<IDatabaseService, DatabaseService>();
			
			MainPage = FreshPageModelResolver.ResolvePageModel<NicknamePageModel>();
			//MainPage = FreshPageModelResolver.ResolvePageModel<VkSongsSourcePageModel>();
		} 

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
