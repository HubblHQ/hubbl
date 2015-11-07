using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Hubl.Daemon.Commands
{
    class HelpCommand:ICommand
    {
        private readonly IContainer _container;

        public HelpCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] {"help", "?"};
            Description = Properties.Resources.HelpCommand;
        }

        public bool Execute(params string[] args)
        {
            foreach (var command in _container.Resolve<IEnumerable<ICommand>>())
            {
                Console.WriteLine("\t{0} - {1}", command.Shortcuts.FirstOrDefault(), command.Description);
            }
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
