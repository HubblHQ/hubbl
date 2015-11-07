using System;
using System.Linq;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Hubl.Mobile.Network;


namespace Hubl.Mobile
{
	class MobileSession : ISession
	{
		private readonly MobileNetworkSettings _settings;
		private readonly User _user;

		public MobileSession(MobileNetworkSettings settings, string userTitle)
		{			
			_settings = settings;

			_user = new User
			{
				Id = Guid.NewGuid().ToString(),
				Title = userTitle
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
