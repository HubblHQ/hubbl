using System;
using System.Threading;
using MessageRouter.Network;
using Hubl.Daemon.Network;
using Hubl.Daemon.Message;
using Autofac;
using Hubl.Core.Service;

namespace Hubl.Daemon.Network
{
	class MainClass
	{
		static IContainer container;

		static IContainer createContainer()
		{
			var builder = new ContainerBuilder ();
			builder.RegisterModule <NetworkModule> ();
			builder.RegisterType<UsersService>()
				.SingleInstance();
			return builder.Build ();
		}

		public static void Main (string[] args)
		{
			container = createContainer ();
			container.Resolve <NetworkSettings> ().TTL = 5;

			var router = container.Resolve<INetworkMessageRouter> ();

			router.Subscribe<StringMessage> ().OnSuccess ((ep, m) => {
				Console.WriteLine ("Get message {0} from {1}", m.StrMessage, ep.Address);
			});



			router.Start ();

			while (true) {
				var str = Console.ReadLine ();

				// Console.WriteLine (str);
				router.Publish (new StringMessage (str)).Run ();
				Console.WriteLine ("published");
				Thread.Sleep (1000);
			}
		}
	}
}
