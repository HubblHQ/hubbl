using System;
using System.Collections.Generic;
using Hubbl.Core.Service;
using Autofac;

namespace Hubbl.Console.Commands
{
    public class ServerVolumeChangeCommand : ICommand
    {
        private readonly IContainer _container;

        public ServerVolumeChangeCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] { "volume", "vol" };
            Description = Properties.Resources.ServerVolumeChangeCommand;
        }

        public bool Execute(params string[] args)
        {
            int volume = 0;
            if (args.Length <= 0 || !Int32.TryParse(args[0], out volume))
            {
                System.Console.WriteLine("Parameters are incorrect. Please, try again.");
            }

            if (MainClass._clientServer.IsServer())
            {
                _container.Resolve<IMusicPlayer>().SetVolume(volume);
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