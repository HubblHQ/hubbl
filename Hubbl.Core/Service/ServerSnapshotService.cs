using System;
using System.Threading;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Hubbl.Core.Messages;
using Hubbl.Core.Model;
using Hubbl.Core.Service;

namespace Hubbl.Console.Service
{
    public class ServerSnapshotService
    {

        private Task _task;
        private bool _isRunned = false;
        private bool _isInterrupted = false;
        private IMusicPlayer _player;
        private INetworkMessageRouter _router;
            
        public ServerSnapshotService(IMusicPlayer player, INetworkMessageRouter router)
        {
            _player = player;
            _router = router;
            _task = new Task(loop);
        }

        private void loop()
        {
            while (!_isInterrupted)
            {
                var snapshot = new ServerSnapshot()
                    { PlayingTrack = _player.CurrentPlayedEntry, Playlist = _player.Playlist, Status = _player.Status};
                lock (_player.Playlist)
                {
                    _router.Publish(new SnapshotMessage(snapshot)).Run();
                }
                Task.Delay(1000);
            }
        }

        public void Run()
        {
            if (_isRunned) throw new Exception("Cannot run ServerSnapshotService twice");
            _isRunned = true;
            Task.Run(() => loop());
        }

        public void Stop()
        {
            _isInterrupted = true;
        }

    }
}