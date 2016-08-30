using System;
using System.Threading.Tasks;
using Autofac;
using Hubbl.Console.Properties;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Module.MessageRouter.Desktop.Network;
using System.Linq;
using Hubbl.Console.Service;
using System.Collections.Generic;

namespace Hubbl.Console.ClientServerModule
{
    public class ClientServerManager
    {

        private IContainer _container;
        private bool _isServer = false;

        public ClientServerManager(IContainer container)
        {
            _container = container;
            StartClientListeners();
        }

        public bool IsServer()
        {
            return _isServer;
        }

        public bool WorkAsClient()
        {
            if (_isServer)
            {
                _container.Resolve<IMusicPlayer>().Stop();
                StopListeners();
                StartClientListeners();
                _isServer = false;
                return true;
            }
            else
            {
                ClearAllUsers();
                _container.Resolve<INetworkMessageRouter>()
                    .Publish(new HelloMessage(_container.Resolve<ISession>().CurrentUser)).Run();
            }
            return false;
        }

        public bool WorkAsServer()
        {
            if (!_isServer)
            {
                StopListeners();
                StartServerListeners();
                _isServer = true;
                _container.Resolve<IMusicPlayer>().Play();
                return true;
            }
            else
            {
                ClearAllUsers();
            }
            return false;
        }

        private void ClearAllUsers()
        {
            var usersService = _container.Resolve<HubblUsersService>();
            usersService.RemoveAll();
        }

        public void StopListeners()
        {
            var router = _container.Resolve<INetworkMessageRouter>();
            Task.Factory.StartNew(() => router.Stop()).Wait();
        }

        private void StartClientListeners()
        {
            var router = _container.Resolve<INetworkMessageRouter>();
            router.Subscribe<EchoMessage>()
                .OnSuccess((rp, m) =>
                {
                    System.Console.WriteLine("Get message {0} from {1}:{2}", m, rp.Address, rp.Port);
                    //m.Sender.IpAddress = rp.Address;
                    _container.Resolve<HubblUsersService>().Add(m.Sender);
                })
                .OnException(e =>
                {
                    System.Console.WriteLine("Exception catched!\n " + e.Message);
                    throw e;
                });

            router.Publish(new HelloMessage(_container.Resolve<ISession>().CurrentUser)).Run();

            router.Subscribe<TextMessage>()
                .OnSuccess((rp, m) =>
                {
                    var user = _container.Resolve<HubblUsersService>().Get(rp);
                    System.Console.WriteLine("{0}:{1}", user != null ? user.Title : Resources.UnknownUser, m.Text);
                }).OnException(
                    e =>
                    {
                        System.Console.WriteLine("Exception catched!\n" + e.Message);
                        throw e;
                    });
            router.Subscribe<SnapshotMessage>()
                .OnSuccess((rp, m) =>
                {
                    var player = _container.Resolve<IMusicPlayer>();
                    player.Playlist = (List<PlaylistEntry>) m.Snapshot.Playlist;
                    player.CurrentPlayedEntry = m.Snapshot.PlayingTrack;
                    player.Status = m.Snapshot.Status;
                }).OnException(
                    e =>
                    {
                        System.Console.WriteLine("Exception catched!\n" + e.Message);
                        throw e;
                    });

            Task.Factory.StartNew(() => router.Start());
        }

        private void StartServerListeners()
        {
            var networkSettings = _container.Resolve<NetworkSettings>();
            networkSettings.TTL = 5;

            var router = _container.Resolve<INetworkMessageRouter>();

            router.Subscribe<HelloMessage>()
                .OnSuccess((ep, m) =>
                {
                    System.Console.WriteLine("Get message {0} from {1}:{2}", m, ep.Address, ep.Port);
                    m.Sender.IpAddress = ep.Address;
                    _container.Resolve<HubblUsersService>().Add(m.Sender);
                    router.PublishFor(new[] { m.Sender.Id }, new EchoMessage(_container.Resolve<ISession>().CurrentUser)).First().Run();
                }).OnException(
                    e =>
                    {
                        System.Console.WriteLine("Exception catched!\n " + e.Message);
                        throw e;
                    });

            router.Subscribe<TextMessage>()
                .OnSuccess((rp, m) =>
                {
                    var user = _container.Resolve<HubblUsersService>().Get(rp);
                    System.Console.WriteLine("{0}:{1}", user != null ? user.Title : Resources.UnknownUser, m.Text);
                }).OnException(
                    e =>
                    {
                        System.Console.WriteLine("Exception catched!");
                        System.Console.WriteLine("	" + e.Message);

                        throw e;
                    });


            router.Subscribe<AddCloudTrackMessage>()
                .OnSuccess((rp, m) =>
                {
                    System.Console.WriteLine("Cloud track request " + m.Track.Source);
                    var entry = _container.Resolve<IMusicPlayer>().QueueTrack(m.Sender, m.Track);

                    System.Console.WriteLine("added {0}", entry);
                }).OnException(
                    e =>
                    {
                        System.Console.WriteLine("Exception catched!");
                        System.Console.WriteLine("	" + e.Message);

                        throw e;
                    });

            Task.Factory.StartNew(() => router.Start());
            _container.Resolve<ServerSnapshotService>().Run();
        }
        
    }
}