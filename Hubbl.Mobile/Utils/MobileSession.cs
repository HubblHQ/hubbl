using System;
using System.Linq;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Hubbl.Mobile.Network;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Hubbl.Mobile
{
	class MobileSession : ISession
	{
		private readonly MobileNetworkSettings _settings;
		private User _user;
		private ObservableCollection<PlaylistEntry> playlist;
		public MobileSession(MobileNetworkSettings settings)
		{			
			_settings = settings;

			_user = new User
			{
				Id = Guid.NewGuid().ToString(),
				Title = "staff"
			};
			playlist = new ObservableCollection<PlaylistEntry> ();

		}

		public User CurrentUser
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
		public List<PlaylistEntry> Playlist
		{
			get
			{
				return playlist.ToList();
			}
		}

	}
}
