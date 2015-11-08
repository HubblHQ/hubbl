using System;
using System.Collections.ObjectModel;
using System.Linq;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Hubl.Daemon.Network;

namespace Hubl.Daemon
{
    class ConsoleSession : ISession
    {
        private readonly NetworkSettings _settings;
        private User _user;

        public ConsoleSession(NetworkSettings settings)
        {
            var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            _settings = settings;
            _user = new User
            {
                Id = interfaces.First().Id,
                Title = Environment.MachineName
            };
            Playlist = new ObservableCollection<Track>();

        }

        public User CurrentUser
        {
            get
            {
                _user.Port = _settings.ListenPort;
                return _user;
            }
            set { _user = value; }
        }

        public ObservableCollection<Track> Playlist { get; private set; }
    }
}
