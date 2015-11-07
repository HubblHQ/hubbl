using System;
using System.Linq;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Hubl.Daemon.Network;

namespace Hubl.Daemon
{
    class ConsoleSession : ISeesion
    {
        private readonly NetworkSettings _settings;
        private readonly User _user;

        public ConsoleSession(NetworkSettings settings)
        {
            var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            _settings = settings;
            _user = new User
            {
                Id = interfaces.First().Id,
                Title = Environment.MachineName
            };

        }

        public User CurrentUser
        {
            get
            {
                _user.Port = _settings.ListenPort;
                return _user;
            }
        }
    }
}
