using System.Collections.Generic;
using System.Linq;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions;

namespace Hubbl.Core.Service
{

	public class HubblUsersService : UsersService<HubblUser>
	{
        List<HubblUser> GetHubsList()
        {
            return (from kvp in _users
                    where !kvp.Value.IsHub
                    select kvp.Value).ToList();
        }
    }
}
