using System.Collections.Generic;
using Hubl.Core.Messages;
using Hubl.Core.Service;
using Hubl.Daemon.Properties;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Daemon.Commands
{
	public class SendTextCommand : ICommand
	{
		private readonly INetworkMessageRouter _route;
		private readonly UsersService _usersService;

		public SendTextCommand(INetworkMessageRouter route, UsersService usersService)
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

		public IEnumerable<string> Shortcuts { get; }
		public string Description { get; }
	}
}
