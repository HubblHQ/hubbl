using System;
using Autofac;
using Hubl.Core.Service;
using Hubl.Core.Model;
using MessageRouter.Network;
using Xamarin.Forms;
using Hubl.Core.Messages;

namespace Hubl.Mobile
{
	public class App : Application
	{	
		public static IContainer Container {get; private set;}
		public static INetworkMessageRouter Router {get; private set; }
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
			Container = CreateContainer ();
			Router = Container.Resolve<INetworkMessageRouter> ();

			MainPage = new NavigationPage (new Hubl.Mobile.NickPage ());
		}

		protected override void OnStart ()
		{	
			try {				
				Router.StartAsync().ContinueWith((a) => {
					Router.Subscribe<HelloMessage>().OnSuccess(m => {
						Container.Resolve<UsersService>().Add(m.Sender);
						var echo = new EchoMessage(Container.Resolve<ISession>().CurrentUser);
						Router.PublishFor(new string[]{m.Sender.Id}, echo);
					}).OnException(m => {
						var aa = m;
					});
					Router.Subscribe<EchoMessage>().OnSuccess(m => {
						var ss = m;
					});
				}
				);
			}
			catch (Exception e) {
				var a = 4;
			}
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			Router.StopAsync ();
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			try {
				Router.StartAsync ().Wait();
			}
			catch (Exception e) {
				var a = 4;
			}
			// Handle when your app resumes
		}

	}
}

