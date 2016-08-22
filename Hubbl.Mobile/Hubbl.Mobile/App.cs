using FreshMvvm;
using Hubbl.Mobile.PageModels;
using Hubbl.Mobile.Services;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Xamarin.Forms;

namespace Hubbl.Mobile
{
	public class App : Application
	{
		public static INetworkMessageRouter Router { get; private set; }


		public App()
		{
			FreshIOC.Container.Register<IDatabaseService, DatabaseService>();
			
			MainPage = FreshPageModelResolver.ResolvePageModel<NicknamePageModel>(); 
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
