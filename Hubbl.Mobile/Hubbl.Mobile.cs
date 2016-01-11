using System;
using System.Linq;
using Autofac;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Hubbl.Mobile.Utils;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;
using Module.MessageRouter.Mobile.Network;
using Xamarin.Forms;

namespace Hubbl.Mobile
{
	public class App : Application
	{	
		public static IContainer Container {get; private set;}
		public static INetworkMessageRouter Router {get; private set; }
		static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder ();
			builder.RegisterModule <NetworkModule> ();
			builder.RegisterType<UsersService<HubblUser>>()
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

			var user = HubblUser.LoadUser ();
			if (user != null) {
				Container.Resolve<ISession> ().CurrentUser = user;
				MainPage = new NavigationPage (new HubsPage ());
			} else {
				MainPage = new NavigationPage (new NickPage ());
		}
		}

		protected override void OnStart ()
		{
			try {
				Router.Subscribe<HelloMessage>().OnSuccess((ep, m) => {
					m.Sender.IpAddress = ep.Address;
					Container.Resolve<UsersService<HubblUser>>().Add(m.Sender);
					var echo = new EchoMessage(Container.Resolve<ISession>().CurrentUser);
					var echoTask = Router.PublishFor(new[] { m.Sender.Id }, echo).First();

					echoTask.OnException(e => {
						var aa = e;
					}).OnSuccess(me => {
						var sd = me;
					});
					echoTask.Run();

				}).OnException(m => {
					var aa = m;
				});

				Router.Subscribe<EchoMessage>().OnSuccess(m => {
					var ss = m;
				});

				Router.Subscribe<AddCloudTrackMessage>().OnSuccess((ep, m) =>
				{
					var currentUser = Container.Resolve<ISession>().CurrentUser;
					if (currentUser.IsHub)
						Container.Resolve<IMusicPlayer>().QueueTrack(m.Sender, m.Track);
				});
			}
			catch (Exception e) {
				var a = 4;
			}
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			Router.Stop ();
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			try {
				Router.Start();
			}
			catch (Exception e) {
				var a = 4;
			}
			// Handle when your app resumes
		}

	}
}

