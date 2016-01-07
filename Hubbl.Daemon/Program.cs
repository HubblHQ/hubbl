using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autofac;
using Hubl.Core.Messages;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Hubl.Daemon.Commands;
using Hubl.Daemon.Properties;
using Hubl.Daemon.Service;
using Module.MessageRouter.Abstractions.Network;
using Module.MessageRouter.Desktop.Network;

namespace Hubl.Daemon
{
	internal class MainClass
	{
		private static IContainer _container;
		private static bool _runing;

		private static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<NetworkModule>();
			builder.RegisterModule<CommandsModule>();
			builder.RegisterType<UsersService>()
				.SingleInstance();
			builder.RegisterType<MPlayerBackend>()
				.As<IMusicPlayerBackend>()
				.SingleInstance();

			builder.RegisterType<HubblPlayer>()
				.As<IMusicPlayer>()
				.SingleInstance();

			builder.RegisterType<ConsoleSession>()
				.As<ISession>()
				.SingleInstance();


			return builder.Build();
		}

		private static void dumpPlaylist(List<PlaylistEntry> playlist)
		{
			var i = 1;
			Console.WriteLine("\nCURRENT PLAYLIST");
			foreach (var entry in playlist)
			{
				Console.WriteLine("{0}. {1} - {2} added by {3}", i++, entry.Track.Artist, entry.Track.Name, entry.User.Title);
			}
		}

		[MTAThread]
		public static int Main(string[] args)
		{
			_container = CreateContainer();
			var builder = new ContainerBuilder();
			builder.RegisterInstance(_container)
				.ExternallyOwned();
			builder.Update(_container);

			var networkSettings = _container.Resolve<NetworkSettings>();
			networkSettings.TTL = 5;

			var router = _container.Resolve<INetworkMessageRouter>();

			router.Subscribe<HelloMessage>()
				.OnSuccess((ep, m) =>
				{
					Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
					m.Sender.IpAddress = ep.Address;
					_container.Resolve<UsersService>().Add(m.Sender);
					router.PublishFor(new[] {m.Sender.Id}, new EchoMessage(_container.Resolve<ISession>().CurrentUser)).First().Run();
				}).OnException(e => { });

			router.Subscribe<EchoMessage>()
				.OnSuccess((ep, m) =>
				{
					Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
					m.Sender.IpAddress = ep.Address;
					_container.Resolve<UsersService>().Add(m.Sender);
				});

			router.Subscribe<TextMessage>()
				.OnSuccess((rp, m) =>
				{
					var user = _container.Resolve<UsersService>().Get(rp);
					Console.WriteLine("{0}:{1}", user != null ? user.Title : Resources.UnknowUser, m.Text);
				});

			router.Subscribe<AddCloudTrackMessage>()
				.OnSuccess((rp, m) =>
				{
					Console.WriteLine("Cloud track request " + m.Track.Source);
					var entry = _container.Resolve<IMusicPlayer>().QueueTrack(m.Sender, m.Track);
					Console.WriteLine("added {0}", entry);
				});


			Task.Factory.StartNew(() => router.Start());

			Console.CancelKeyPress += ConsoleOnCancelKeyPress;
			_runing = true;

			var users = _container.Resolve<UsersService>();
			var player = _container.Resolve<IMusicPlayer>();

			while (_runing)
			{
				Console.Write("hubl>: ");
				var commandLine = Regex.Split(Console.ReadLine() ?? string.Empty, "\\s");
				var commands = _container.Resolve<IEnumerable<ICommand>>();
				var cmd = commands.FirstOrDefault(m => m.Shortcuts.Contains(commandLine.FirstOrDefault()));
				if (cmd != null)
				{
					if (cmd.Execute(commandLine.Skip(1).ToArray()))
						break;
				}
				else Console.WriteLine(Resources.InvalidCommand);
			}
			Task.Factory.StartNew(() => router.Stop()).Wait();

			_container.Dispose();
			return 0;
		}


		private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			_runing = false;
		}
	}
}