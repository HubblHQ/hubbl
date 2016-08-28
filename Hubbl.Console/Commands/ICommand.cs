using System.Collections.Generic;

namespace Hubbl.Console.Commands
{
    interface ICommand
    {
        bool Execute(params string[] args);

        IEnumerable<string> Shortcuts { get; }

        string Description { get; }
    }
}
