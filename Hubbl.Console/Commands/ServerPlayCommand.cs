using System.Collections.Generic;
using Hubbl.Core.Service;
using Autofac;

namespace Hubbl.Console.Commands
{
    public class ServerPlayCommand : ICommand
    {
        private readonly IContainer _container;

        public ServerPlayCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] { "play", "pl" };
            Description = Properties.Resources.ServerPlayCommand;
        }

        public bool Execute(params string[] args)
        {
            if (MainClass._clientServer.IsServer())
            {
                _container.Resolve<IMusicPlayer>().Play();
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