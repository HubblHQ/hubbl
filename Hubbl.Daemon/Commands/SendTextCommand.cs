using System.Collections.Generic;
using Hubbl.Core.Messages;
using Hubbl.Core.Service;
using Hubbl.Daemon.Properties;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;
using Module.MessageRouter.Abstractions.Network.Interfaces;

namespace Hubbl.Daemon.Commands
{
	public class SendTextCommand : ICommand
	{
		private readonly INetworkMessageRouter _route;
		private readonly IUsersService _usersService;

		public SendTextCommand(INetworkMessageRouter route, IUsersService usersService)
		{
			_route = route;
			_usersService = usersService;
			Shortcuts = new[] {"text"};
			Description = Resources.TextCommand;
		}

		public bool Execute(params string[] args)
		{
			_route.Publish(new TextMessage(string.Join(" ", args))).Run();
			return false;
		}

		public IEnumerable<string> Shortcuts { get; private set; }
		public string Description { get; private set;}
	}
}
