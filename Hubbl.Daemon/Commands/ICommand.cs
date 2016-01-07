using System.Collections.Generic;

namespace Hubbl.Daemon.Commands
{
    interface ICommand
    {
        bool Execute(params string[] args);

        IEnumerable<string> Shortcuts { get; }

        string Description { get; }
    }
}
