using System;
using System.Linq;
using Hubl.Core.Model;
using Hubl.Core.Service;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using Module.MessageRouter.Desktop.Network;

namespace Hubl.Daemon
{
	class ConsoleSession : ISession
	{
		private readonly NetworkSettings _settings;
		private User _user;
		private IMusicPlayer _player;

		public ConsoleSession(NetworkSettings settings, IMusicPlayer player)
		{
			_player = player;
			var interfaces = NetworkInterface.GetAllNetworkInterfaces();
			_settings = settings;
			_user = new User
			{
				Id = interfaces.First().Id,
				Title = Environment.MachineName,
				IsHub = true,
				Hub = Environment.MachineName
			};

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

		public List<PlaylistEntry> Playlist {
			get {
				return _player.Playlist;
			}
		}
	}
}
