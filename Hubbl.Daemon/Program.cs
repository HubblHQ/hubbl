using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autofac;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Hubbl.Daemon.Commands;
using Hubbl.Daemon.Properties;
using Hubbl.Daemon.Service;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Module.MessageRouter.Desktop.Network;

namespace Hubbl.Daemon
{
	internal static class MainClass
	{
		private static IContainer _container;
		private static bool _running;

		private static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<NetworkModule>();
			builder.RegisterModule<CommandsModule>();
			builder.RegisterType<HubblUsersService>()
				.As<IUsersService>()
				.As<HubblUsersService>()
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
			builder.RegisterInstance(_container).ExternallyOwned();
			builder.Update(_container);

			var networkSettings = _container.Resolve<NetworkSettings>();
			networkSettings.TTL = 5;

			var router = _container.Resolve<INetworkMessageRouter>();

			router.Subscribe<HelloMessage>()
				.OnSuccess((ep, m) =>
				{
					Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
					m.Sender.IpAddress = ep.Address;
					_container.Resolve<HubblUsersService>().Add(m.Sender);
					router.PublishFor(new[] {m.Sender.Id}, new EchoMessage(_container.Resolve<ISession>().CurrentUser)).First().Run();
				}).OnException(
					e =>
					{
						Debug.WriteLine("Exception catched!");
						Debug.WriteLine("	" + e.Message);

						throw e;
					});
			;

			router.Subscribe<EchoMessage>()
				.OnSuccess((ep, m) =>
				{
					Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
					m.Sender.IpAddress = ep.Address;
					_container.Resolve<HubblUsersService>().Add(m.Sender);
				}).OnException(
					e =>
					{
						Debug.WriteLine("Exception catched!");
						Debug.WriteLine("	" + e.Message);

						throw e;
					});
			;

			router.Subscribe<TextMessage>()
				.OnSuccess((rp, m) =>
				{
					var user = _container.Resolve<HubblUsersService>().Get(rp);
					Console.WriteLine("{0}:{1}", user != null ? user.Title : Resources.UnknownUser, m.Text);
				}).OnException(
					e =>
					{
						Debug.WriteLine("Exception catched!");
						Debug.WriteLine("	" + e.Message);

						throw e;
					});
			;

			router.Subscribe<AddCloudTrackMessage>()
				.OnSuccess((rp, m) =>
				{
					Console.WriteLine("Cloud track request " + m.Track.Source);
					var entry = _container.Resolve<IMusicPlayer>().QueueTrack(m.Sender, m.Track);
					Console.WriteLine("added {0}", entry);
				}).OnException(
					e =>
					{
						Debug.WriteLine("Exception catched!");
						Debug.WriteLine("	" + e.Message);

						throw e;
					});
			;

			router.Subscribe<SendFileMessage>()
				.OnSuccess((rp, m) =>
				{
					Console.WriteLine("We got the file transfer! name:  " + m.Filename);
					Console.WriteLine("	Trying to save...");

					var fileStream = File.Create("files/", (int) m.StreamLength);

					var bytesInStream = new byte[(int) m.Stream.Length];

					m.Stream.Read(bytesInStream, 0, bytesInStream.Length);

					fileStream.Write(bytesInStream, 0, bytesInStream.Length);
				}).OnException(
					e =>
					{
						Debug.WriteLine("Exception catched!");
						Debug.WriteLine("	" + e.Message);

						throw e;
					});


			Task.Factory.StartNew(() => router.Start());

			Console.CancelKeyPress += ConsoleOnCancelKeyPress;
			_running = true;

			var users = _container.Resolve<IUsersService>();
			var player = _container.Resolve<IMusicPlayer>();

			while (_running)
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
			_running = false;
		}
	}
}