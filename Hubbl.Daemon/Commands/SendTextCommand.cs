using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hubl.Core.Messages;
using Hubl.Core.Service;
using MessageRouter.Network;

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
            Description = Properties.Resources.TextCommand;
        }

        public bool Execute(params string[] args)
        {
            _route.Publish(new TextMessage(string.Join(" ", args))).Run();
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
