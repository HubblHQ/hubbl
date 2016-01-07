using System.Collections.Generic;
using Hubl.Core.Messages;
using Hubl.Core.Service;
using Hubl.Daemon.Properties;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Daemon.Commands
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

		public IEnumerable<string> Shortcuts { get; }
		public string Description { get; }
	}
}
