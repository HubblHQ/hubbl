using System.Collections.Generic;
using Autofac;
using Hubbl.Core.Model;
using Hubbl.Core.Service;

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
            PlaylistEntry current;

            if (MainClass._clientServer.IsServer())
            {
                current = _container.Resolve<IMusicPlayer>().CurrentPlayedEntry;
                list = _container.Resolve<IMusicPlayer>().Playlist;
            }
            else
            {
                current = null;
                list = new List<PlaylistEntry>();
                System.Console.WriteLine("Not supported yet");
            }

            System.Console.WriteLine();
            if (current != null)
            {
                System.Console.WriteLine(current);
            }
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
