using System;
using Autofac;
using Hubl.Core.Service;
using System.Linq;
using Hubl.Core.Model;
using MessageRouter.Network;

using Hubl.Mobile.Network;

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

			var user = User.LoadUser ();
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
				Router.StartAsync().ContinueWith((a) => {
					Router.Subscribe<HelloMessage>().OnSuccess((ep,m) => {
						m.Sender.IpAddress = ep.Address;
						Container.Resolve<UsersService>().Add(m.Sender);
						var echo = new EchoMessage(Container.Resolve<ISession>().CurrentUser);
						var echoTask = Router.PublishFor(new string[] {m.Sender.Id}, echo).First ();
						echoTask.OnException ((e) => {
							var aa = e;
						}).OnSuccess ((me) => {
							var sd = me;
						});
						echoTask.Run ();

					}).OnException(m => {
						var aa = m;
					});
					Router.Subscribe<EchoMessage>().OnSuccess(m => {
						var ss = m;
					});
					Router.Subscribe<AddCloudTrackMessage> ().OnSuccess ((ep, m) => {
						var currentUser = App.Container.Resolve<ISession> ().CurrentUser;
						if (currentUser.IsHub) {
							var playlist = App.Container.Resolve<ISession>().Playlist;
							playlist.Add (m.Track);
						}
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
				Router.StartAsync ();
			}
			catch (Exception e) {
				var a = 4;
			}
			// Handle when your app resumes
		}

	}
}

