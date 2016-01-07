using System.Collections.Generic;
using System.Linq;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Service
{

	public class HubblUsersService : IUsersService
	{
		private readonly Dictionary<string, HubblUser> _users = new Dictionary<string, HubblUser>();

		public IUser Get(string id)
		{
			return GetHubblUser(id);
		}

		public HubblUser GetHubblUser(string id)
		{
			return _users.ContainsKey(id) ? _users[id] : null;
		}

		public bool Remove(string id)
		{
			return _users.Remove(id);
		}

		public bool Add(HubblUser user)
		{
			if (_users.ContainsKey(user.Id))
				return false;
			
			_users.Add(user.Id, user);
			return true;
		}

		public IEnumerable<HubblUser> GetList()
		{
			return _users.Values.ToList();
		}

		public IEnumerable<string> GetUserIds()
		{
			return _users.Values.Select(user => user.Id).ToList();
		}

		public HubblUser Get(RemotePoint remotePoint)
		{
			return _users.Values.FirstOrDefault(m => m.IpAddress == remotePoint.Address);
		}
	}
}
