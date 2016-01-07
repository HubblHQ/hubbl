using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Module.MessageRouter.Desktop.Network;

namespace Hubl.Daemon.Network
{
    class SystemMulticastClient:IMulticastClient
    {
        private readonly NetworkSettings _settings;
        private readonly UdpClient _client;

        public SystemMulticastClient(NetworkSettings settings)
        {
            
            _settings = settings;
            _client = new UdpClient(new IPEndPoint(IPAddress.Any, settings.MulticastPort)) {Ttl = ((short) settings.TTL)};
            _client.MulticastLoopback = true;
            //_client.EnableBroadcast = true;
        }

        private void OnMessageReceived(object sender, DatagramReceivedEventArgs e)
        {
            if(MessageReceived != null)
                MessageReceived(sender, e);
        }

        public void Dispose()
        {
            //_client.DropMulticastGroup(IPAddress.Parse(_settings.MulticastAddress));
            _client.Close();
        }


        public event EventHandler<DatagramReceivedEventArgs> MessageReceived;

        public async Task JoinMulticastGroupAsync()
        {
            await Task.Run(() => _client.JoinMulticastGroup(IPAddress.Parse(_settings.MulticastAddress)));
            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var result = await _client.ReceiveAsync();
                    OnMessageReceived(_client,
                        new DatagramReceivedEventArgs(result.RemoteEndPoint.Address.ToString(),
                            result.RemoteEndPoint.Port, result.Buffer));
                }
            });

        }

        public Task DisconnectAsync()
        {
           // return Task.Run(() => _client.DropMulticastGroup(IPAddress.Parse(_settings.MulticastAddress)));
            return Task.Run(() => { });
        }

        public Task SendMulticastAsync(byte[] data)
        {
            return _client.SendAsync(data, data.Length, new IPEndPoint(IPAddress.Parse(_settings.MulticastAddress), _settings.MulticastPort));
        }
    }
}
