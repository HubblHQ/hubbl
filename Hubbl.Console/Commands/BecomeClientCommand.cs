using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Hubbl.Console.Commands
{
    class BecomeClientCommand : ICommand
    {
        private readonly IContainer _container;

        public BecomeClientCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] { "start_client", "client" };
            Description = Properties.Resources.BecomeClientCommand;
        }

        public bool Execute(params string[] args)
        {
            if (!Console.MainClass._clientServer.WorkAsClient())
            {
                System.Console.WriteLine("Already client");
                return false;
            }
            System.Console.WriteLine("Client now");
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
