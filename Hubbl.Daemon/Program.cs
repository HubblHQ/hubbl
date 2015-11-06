using System;
using System.Linq;
using System.Threading;
using Autofac;
using Hubl.Core.Messages;
using Hubl.Core.Service;
using Hubl.Daemon.Message;
using Hubl.Daemon.Network;
using MessageRouter.Network;

namespace Hubl.Daemon
{
	class MainClass
	{
		static IContainer _container;

		static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder ();
			builder.RegisterModule <NetworkModule> ();
			builder.RegisterType<UsersService>()
				.SingleInstance();
		    
            builder.RegisterType<ConsoleSession>()
		        .As<ISeesion>()
		        .SingleInstance();

			return builder.Build ();
		}

		public static void Main (string[] args)
		{
			_container = CreateContainer ();
            var networkSettings = _container.Resolve <NetworkSettings> ();
		    networkSettings.TTL = 5;

			var router = _container.Resolve<INetworkMessageRouter> ();

		    router.Subscribe<HelloMessage>()
                .OnSuccess((ep, m) =>
		    {
		        Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
		        m.Sender.IpAddress = ep.Address;
		        _container.Resolve<UsersService>().Add(m.Sender);
                router.PublishFor(new []{m.Sender.Id}, new EchoMessage(_container.Resolve<ISeesion>().CurrentUser)).First().Run();
		    });
		    router.Subscribe<HelloMessage>()
		        .OnSuccess((ep, m) =>
		        {
		            Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
		            m.Sender.IpAddress = ep.Address;
		            _container.Resolve<UsersService>().Add(m.Sender);
		        });

			router.Start ();
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;

			while (true) {
				var str = Console.ReadLine ();

				// Console.WriteLine (str);
				router.Publish (new StringMessage (str)).Run ();
				Console.WriteLine ("published");
			}
		}

	    private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
	    {
	        var networkMessageRouter = _container.Resolve<INetworkMessageRouter>();
            networkMessageRouter.Stop();
	        _container.Dispose();
	    }
	}
}
