using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hubl.Core.Messages;
using Hubl.Core.Service;
using MessageRouter.Network;

namespace Hubl.Daemon.Commands
{
    class HelloCommand:ICommand
    {
        private readonly INetworkMessageRouter _router;
        private readonly ISeesion _session;

        public HelloCommand(INetworkMessageRouter router, ISeesion session)
        {
            _router = router;
            _session = session;
            Shortcuts = new[] {"hello"};
            Description = Properties.Resources.HelloCommand;
        }

        public bool Execute(params string[] args)
        {
            _router.Publish(new HelloMessage(_session.CurrentUser)).Run();
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
