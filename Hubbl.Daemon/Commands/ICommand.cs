using System.Collections.Generic;

namespace Hubl.Daemon.Commands
{
    interface ICommand
    {
        bool Execute(params string[] args);

        IEnumerable<string> Shortcuts { get; }
    }
}
