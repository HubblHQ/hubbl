using System.Collections.Generic;
using Hubbl.Core.Messages;
using Hubbl.Core.Service;
using Hubbl.Daemon.Properties;
using Module.MessageRouter.Abstractions.Network;
using Module.MessageRouter.Abstractions.Network.Interfaces;

namespace Hubbl.Daemon.Commands
{
	internal class HelloCommand : ICommand
	{
		private readonly INetworkMessageRouter _router;
		private readonly ISession _session;

		public HelloCommand(INetworkMessageRouter router, ISession session)
		{
			_router = router;
			_session = session;
			Shortcuts = new[] {"hello"};
			Description = Resources.HelloCommand;
		}

		public bool Execute(params string[] args)
		{
			_router.Publish(new HelloMessage(_session.CurrentUser)).Run();
			return false;
		}

		public IEnumerable<string> Shortcuts { get; private set;}
		public string Description { get; private set;}
	}
}
