using System.Collections.Generic;
using Hubbl.Core.Service;
using Autofac;

namespace Hubbl.Console.Commands
{
    public class ServerPauseCommand : ICommand
    {
        private readonly IContainer _container;

        public ServerPauseCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] { "pause", "ps" };
            Description = Properties.Resources.ServerPauseCommand;
        }

        public bool Execute(params string[] args)
        {
            if (MainClass._clientServer.IsServer())
            {
                _container.Resolve<IMusicPlayer>().Pause();
            }
            else
            {
                System.Console.WriteLine("You are not server");
            }
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}