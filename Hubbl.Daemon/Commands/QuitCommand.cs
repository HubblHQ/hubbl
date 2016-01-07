using System.Collections.Generic;

namespace Hubbl.Daemon.Commands
{
    public class QuitCommand:ICommand
    {
        public QuitCommand()
        {
            Shortcuts = new[] {"quit", "q"};
            Description = Properties.Resources.QuitCommand;
        }
        public bool Execute(params string[] args)
        {
            return true;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
