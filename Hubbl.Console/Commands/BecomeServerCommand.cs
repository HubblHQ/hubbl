using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Hubbl.Console.Commands
{
    class BecomeServerCommand : ICommand
    {
        private readonly IContainer _container;

        public BecomeServerCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] { "start_server", "server" };
            Description = Properties.Resources.BecomeClientCommand;
        }

        public bool Execute(params string[] args)
        {
            if (!Console.MainClass._clientServer.WorkAsServer())
            {
                System.Console.WriteLine("Already server");
                return false;
            }
            System.Console.WriteLine("Server now");
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
