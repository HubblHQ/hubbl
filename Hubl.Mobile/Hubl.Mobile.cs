using System;
using Autofac;
using Hubl.Core.Service;
using Hubl.Mobile.Network;
using Xamarin.Forms;

namespace Hubl.Mobile
{
	public class App : Application
	{	
		public static IContainer Container {get; private set;}
		static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder ();
			builder.RegisterModule <NetworkModule> ();
			builder.RegisterType<UsersService>()
				.SingleInstance();

			builder.RegisterType<MobileSession>()
				.As<ISession>()
				.SingleInstance();

			return builder.Build ();
		}

		public App ()
		{				
			MainPage = new NavigationPage (new Hubl.Mobile.NickPage ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

