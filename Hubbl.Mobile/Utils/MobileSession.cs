using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Hubbl.Mobile.Network;

namespace Hubbl.Mobile.Utils
{
	class MobileSession : ISession
	{
		private readonly MobileNetworkSettings _settings;
		private HubblUser _user;
		private ObservableCollection<PlaylistEntry> _playlist;
		public MobileSession(MobileNetworkSettings settings)
		{			
			_settings = settings;

			_user = new HubblUser
			{
				Id = Guid.NewGuid().ToString(),
				Title = "staff"
			};
			_playlist = new ObservableCollection<PlaylistEntry> ();

		}

		public HubblUser CurrentUser
		{
			get
			{
				_user.Port = _settings.ListenPort;
				return _user;
			}
			set
			{ 
				_user = value;	
			}
		}
		public List<PlaylistEntry> Playlist => _playlist.ToList();
	}
}
