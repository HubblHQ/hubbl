﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Hubbl.Console.Commands;
using Hubbl.Console.Properties;
using Hubbl.Console.Service;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network.Interfaces;

namespace Hubbl.Console.Commands
{
    public class AddCloudTrackCommand : ICommand
    {
        private readonly IContainer _container;

        public AddCloudTrackCommand(IContainer container)
        {
            _container = container;
            Shortcuts = new[] { "add_track", "add" };
            Description = Properties.Resources.AddCloudTrackCommand;
        }

        public bool Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Parameters are incorrect. Please, try again.");
                return false;
            }

            Track t = _container.Resolve<IMusicPlayerBackend>().GetTrackInfo(args[0]);
            //t.SourceType = SourceType.VK;
            if (MainClass._clientServer.IsServer())
            {
                _container.Resolve<IMusicPlayer>().QueueTrack(_container.Resolve<ISession>().CurrentUser, t);
            }
            else
            {
                _container.Resolve<INetworkMessageRouter>() .Publish(
                    new AddCloudTrackMessage { Sender = _container.Resolve<ISession>().CurrentUser, Track = t }).Run();
            }
            
            return false;
        }

        public IEnumerable<string> Shortcuts { get; private set; }
        public string Description { get; private set; }
    }
}