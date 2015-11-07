using System;
using System.Linq;
using Hubl.Core.Model;
using Hubl.Core.Service;


namespace Hubl.Mobile
{
	class MobileSession : ISession
	{
		private readonly MobileNetworkSettings _settings;
		private readonly User _user;

		public MobileSession(MobileNetworkSettings settings)
		{			
			_settings = settings;

			_user = new User
			{
				Id = Guid.NewGuid().ToString(),
				Title = "staff"
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
