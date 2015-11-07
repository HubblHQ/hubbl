using System;
using System.Threading.Tasks;
using Hubl.Core.Service;
using MessageRouter.Network;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Hubl.Mobile.Network
{
	public class MobileTcpListener : ITcpListener
	{
		private readonly MobileNetworkSettings _settings;
		private readonly TcpSocketListener _listener;
		private readonly UsersService _userService;


		public MobileTcpListener (MobileNetworkSettings settings, UsersService userService)
		{
			_settings = settings;
			_listener = new TcpSocketListener ();
			_userService = userService;
            _listener.ConnectionReceived = OnConnectionReceived;
		}

	    private void OnConnectionReceived(object sender, TcpSocketListenerConnectEventArgs e)
	    {
	        if (ConnectionReceived != null)
	        {
                var remotePoint = new RemotePoint(e.SocketClient.RemotePort, e.SocketClient.RemoteAddress);
	            ConnectionReceived(this,
	                new ListenerConnectEventArgs(remotePoint.Address, remotePoint.Port,
	                    new MobileTcpRemoteClient(remotePoint, e.SocketClient)));
	        }
	    }

	    #region ITcpListener
		public event EventHandler<ListenerConnectEventArgs> ConnectionReceived;

		public Task StartListeningAsync ()
		{
			return _listener.StartListeningAsync (_settings.ListenPort);
		}

		public Task StopListeningAsync ()
		{
			return _listener.StopListeningAsync ();
		}
		#endregion

		#region IDisposable implementation
		public void Dispose ()
		{
			_listener.Dispose ();
		}
		#endregion
	}
}

