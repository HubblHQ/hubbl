using System;
using System.Collections.Generic;
using Hubl.Core.Service;

namespace Hubl.Daemon.Commands
{
    class UserListCommand:ICommand
    {
        private readonly UsersService _usersService;

        public UserListCommand(UsersService usersService)
        {
            _usersService = usersService;
            Shortcuts = new[] {"user-list", "ul"};
            Description = Properties.Resources.ListUserCommand;
        }

        public bool Execute(params string[] args)
        {
            foreach (var u in _usersService.GetList())
            {
                Console.WriteLine("{0} id:{1}, ip:{2}:{3}", u.Title, u.Id, u.IpAddress, u.Port);
            }
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}
