using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Autofac;
using Hubl.Core.Messages;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Hubl.Daemon.Commands;
using Hubl.Daemon.Network;
using Hubl.Daemon.Service;
using MessageRouter.Network;

namespace Hubl.Daemon
{
	class MainClass
	{
        static IContainer _container;
	    private static bool _runing;

		static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder ();
			builder.RegisterModule <NetworkModule> ();
		    builder.RegisterModule<CommandsModule>();
			builder.RegisterType<UsersService>()
				.SingleInstance();
		    builder.RegisterType<MPlayerBackend>()
                .As<IMusicPlayerBackend>()
               .SingleInstance();
		    
            builder.RegisterType<ConsoleSession>()
		        .As<ISession>()
		        .SingleInstance();

			return builder.Build ();
		}

		public static int Main (string[] args)
		{
			_container = CreateContainer ();
            var builder = new ContainerBuilder();
		    builder.RegisterInstance(_container)
		        .ExternallyOwned();
            builder.Update(_container);

            var networkSettings = _container.Resolve <NetworkSettings> ();
		    networkSettings.TTL = 10;

			var router = _container.Resolve<INetworkMessageRouter> ();

		    router.Subscribe<HelloMessage>()
                .OnSuccess((ep, m) =>
		    {
		        Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
		        m.Sender.IpAddress = ep.Address;
		        _container.Resolve<UsersService>().Add(m.Sender);
                router.PublishFor(new []{m.Sender.Id}, new EchoMessage(_container.Resolve<ISession>().CurrentUser)).First().Run();
		    });

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
                    Console.WriteLine("{0}:{1}", user != null ? user.Title: Properties.Resources.UnknowUser, m.Text);
		        });

			router.Start ();
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;
		    _runing = true;
			while (_runing)
			{
			    Console.Write("hubl>: ");
			    var commandLine = Regex.Split(Console.ReadLine(), "\\s");
			    var commands = _container.Resolve<IEnumerable<ICommand>>();
			    var cmd = commands.FirstOrDefault(m => m.Shortcuts.Contains(commandLine.FirstOrDefault()));
			    if (cmd != null)
			    {
			        if (cmd.Execute(commandLine.Skip(1).ToArray()))
			            break;
			    }
                else Console.WriteLine(Properties.Resources.InvalidCommand);

			}
            router.Stop();
		    _container.Dispose();
            return 0;
		}

	    private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
	    {
	        _runing = false;
	    }
	}
}
