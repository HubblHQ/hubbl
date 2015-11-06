using System;
using System.Threading;
using Autofac;
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
			return builder.Build ();
		}

		public static void Main (string[] args)
		{
			_container = CreateContainer ();
		    var networkSettings = _container.Resolve <NetworkSettings> ();
		    networkSettings.TTL = 5;

			var router = _container.Resolve<INetworkMessageRouter> ();

			router.Subscribe<StringMessage> ().OnSuccess ((ep, m) => {
				Console.WriteLine ("Get message {0} from {1}:{2}", m.StrMessage, ep.Address, ep.Port);
			});



			router.Start ();

			while (true) {
				var str = Console.ReadLine ();

				// Console.WriteLine (str);
				router.Publish (new StringMessage (str)).Run ();
				Console.WriteLine ("published");
				//Thread.Sleep (1000);
			}
		}
	}
}
