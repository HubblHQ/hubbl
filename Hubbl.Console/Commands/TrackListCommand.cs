using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Hubbl.Console.Commands;
using Hubbl.Console.Properties;
using Hubbl.Console.Service;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network.Interfaces;

namespace Hubbl.Console.Commands
{
    class TrackListCommand : ICommand
    {
        private readonly IContainer _container;

        public TrackListCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] { "tracklist", "tracks" };
            Description = Properties.Resources.TrackListCommand;
        }

        public bool Execute(params string[] args)
        {
            List<PlaylistEntry> list;

            if (MainClass._clientServer.IsServer())
            {
                list = _container.Resolve<IMusicPlayer>().Playlist;
            }
            else
            {
                list = new List<PlaylistEntry>();
                System.Console.WriteLine("Not supported yet");
            }

            System.Console.WriteLine();
            foreach (var el in list)
            {
                System.Console.WriteLine(el);
            }

            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
