using System;
using System.Diagnostics;
using System.Linq;
using Autofac;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Hubbl.Mobile.Utils;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Module.MessageRouter.Mobile.Network;
using Xamarin.Forms;

namespace Hubbl.Mobile
{
	public class App : Application
	{
		public static IContainer Container { get; private set; }
		public static INetworkMessageRouter Router { get; private set; }

		private static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<NetworkModule>();
			builder.RegisterType<UsersService<HubblUser>>()
				.As<IUsersService>()
				.SingleInstance();

			builder.RegisterType<MobileSession>()
				.As<ISession>()
				.SingleInstance();

			return builder.Build();
		}

		public App()
		{
			Container = CreateContainer();
			Router = Container.Resolve<INetworkMessageRouter>();

			var user = HubblUser.LoadUser();
			if (user != null)
			{
				Container.Resolve<ISession>().CurrentUser = user;
				MainPage = new NavigationPage(new HubsPage());
			}
			else
			{
				MainPage = new NavigationPage(new NickPage());
			}
		}

		protected override void OnStart()
		{
			try
			{
				Router.Subscribe<HelloMessage>()
					.OnSuccess((ep, m) =>
					{
						m.Sender.IpAddress = ep.Address;
						Container.Resolve<UsersService<HubblUser>>().Add(m.Sender);

						//Sending EchoMessage back
						var echo = new EchoMessage(Container.Resolve<ISession>().CurrentUser);
						var echoTask = Router.PublishFor(new[] {m.Sender.Id}, echo).First();

						echoTask.OnException(
							e =>
							{
								Debug.WriteLine("Exception catched!");
								Debug.WriteLine("	" + e.Message);

								#if DEBUG
								throw e;
								#endif
							}
							).OnSuccess(
								me =>
								{
									#if DEBUG
									Debug.WriteLine("ECHO Message " + me.Sender);
									#endif
								}
							);

						echoTask.Run();
					})
					.OnException(
						e =>
						{
							Debug.WriteLine("Exception catched!");
							Debug.WriteLine("	" + e.Message);

							#if DEBUG
							throw e;
							#endif
						}
					);

				Router.Subscribe<EchoMessage>()
					.OnSuccess(m =>
					{
						Debug.WriteLine("We got ECHO here!! From: {0}", m.Sender.Title);
					}
					);

				Router.Subscribe<AddCloudTrackMessage>()
					.OnSuccess((ep, m) =>
							{
								var currentUser = Container.Resolve<ISession>().CurrentUser;
								if (currentUser.IsHub)
									Container.Resolve<IMusicPlayer>()
									.QueueTrack(m.Sender, m.Track);
							});
			}
			catch (Exception e)
			{
				Debug.WriteLine("Exception catched!");
				Debug.WriteLine("	" + e.Message);

				#if DEBUG
				throw;
				#endif
			}
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			Router.Stop();
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
				Router.Start();
			// Handle when your app resumes
		}
	}
}

