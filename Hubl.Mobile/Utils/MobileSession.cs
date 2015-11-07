using System;
using System.Linq;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Hubl.Mobile.Network;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Hubl.Mobile
{
	class MobileSession : ISession
	{
		private readonly MobileNetworkSettings _settings;
		private readonly User _user;
		private ObservableCollection<Track> playlist;
		public MobileSession(MobileNetworkSettings settings)
		{			
			_settings = settings;

			_user = new User
			{
				Id = Guid.NewGuid().ToString(),
				Title = "staff"
			};
			playlist = new ObservableCollection<Track> ();

		}

		public User CurrentUser
		{
			get
			{
				_user.Port = _settings.ListenPort;
				return _user;
			}
		}
		public ObservableCollection<Track> Playlist
		{
			get
			{
				return playlist;
			}
		}

	}
}
