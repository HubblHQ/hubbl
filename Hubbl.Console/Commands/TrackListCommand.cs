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
            var player = _container.Resolve<IMusicPlayer>();
            PlaylistEntry current = player.CurrentPlayedEntry;
            List<PlaylistEntry>  list = player.Playlist;

            System.Console.WriteLine();
            if (current != null)
            {
                System.Console.WriteLine("|> " + current);
            }
            lock (list)
            {
                foreach (var el in list)
                {
                    System.Console.WriteLine(el);
                }
            }

            System.Console.WriteLine(player.Status);

            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
