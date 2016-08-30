using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autofac;
using Hubbl.Console.Commands;
using Hubbl.Console.Properties;
using Hubbl.Console.Service;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Module.MessageRouter.Desktop.Network;
using System.Collections.ObjectModel;
using Hubbl.Console.ClientServerModule;
using Hubbl.Console.Tests;

namespace Hubbl.Console
{
    internal static class MainClass
    {
        private static IContainer _container;
        private static bool _running;
        private static bool _isServer = false;
        internal static ClientServerManager _clientServer;

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

            builder.RegisterType<ServerSnapshotService>()
                .SingleInstance();

            return builder.Build();
        }

        private static void DumpPlaylist(List<PlaylistEntry> playlist)
        {
            var i = 1;
            System.Console.WriteLine("\nCURRENT PLAYLIST");
            foreach (var entry in playlist)
            {
                System.Console.WriteLine("{0}. {1} - {2} added by {3}", i++, entry.Track.Artist, entry.Track.Name, entry.User.Title);
            }
        }

       

        [MTAThread]
        public static int Main(string[] args)
        {

            //new ConsoleTest().Run();

            _container = CreateContainer();
            var builder = new ContainerBuilder();
            builder.RegisterInstance(_container).ExternallyOwned();
            builder.Update(_container);

            _clientServer = new ClientServerManager(_container);

            System.Console.CancelKeyPress += ConsoleOnCancelKeyPress;
            _running = true;

            var users = _container.Resolve<IUsersService>();
            var player = _container.Resolve<IMusicPlayer>();

            while (_running)
            {
                System.Console.Write("hubl>: ");
                var commandLine = Regex.Split(System.Console.ReadLine() ?? string.Empty, "\\s");
                var commands = _container.Resolve<IEnumerable<ICommand>>();
                var cmd = commands.FirstOrDefault(m => m.Shortcuts.Contains(commandLine.FirstOrDefault()));
                if (cmd != null)
                {
                    if (cmd.Execute(commandLine.Skip(1).ToArray()))
                        break;
                }
                else System.Console.WriteLine(Resources.InvalidCommand);
            }

            _clientServer.StopListeners();
            _container.Dispose();

            return 0;
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            _running = false;
        }
    }
}
