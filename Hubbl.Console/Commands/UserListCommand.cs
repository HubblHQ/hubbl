using System;
using System.Collections.Generic;
using Hubbl.Core.Service;

namespace Hubbl.Console.Commands
{
    class UserListCommand:ICommand
    {
        private readonly HubblUsersService _usersService;

        public UserListCommand(HubblUsersService usersService)
        {
            _usersService = usersService;
            Shortcuts = new[] {"user-list", "ul"};
            Description = Properties.Resources.ListUserCommand;
        }

        public bool Execute(params string[] args)
        {
            foreach (var u in _usersService.GetList())
            {
                System.Console.WriteLine("{0} id:{1}, ip:{2}:{3}", u.Title, u.Id, u.IpAddress, u.Port);
            }
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
