using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Module.MessageRouter.Desktop.Network;

namespace Hubbl.Daemon
{
	class ConsoleSession : ISession
	{
		private readonly NetworkSettings _settings;
		private HubblUser _user;
		private IMusicPlayer _player;

		public ConsoleSession(NetworkSettings settings, IMusicPlayer player)
		{
			_player = player;
			var interfaces = NetworkInterface.GetAllNetworkInterfaces();
			_settings = settings;
			_user = new HubblUser
			{
				Id = interfaces.First().Id,
				Title = Environment.MachineName,
				IsHub = true,
				Hub = Environment.MachineName
			};

		}

		public HubblUser CurrentUser
		{
			get
			{
				_user.Port = _settings.ListenPort;
				return _user;
			}
			set { _user = value; }
		}

		public List<PlaylistEntry> Playlist {
			get {
				return _player.Playlist;
			}
		}
	}
}
