using System;
using MessageRouter.Network;
using System.Threading.Tasks;
using Sockets.Plugin;

namespace Hubl.Mobile
{
	public class MobileTcpListener : ITcpClient
	{
		

		private readonly MobileNetworkSettings _settings;
		private TcpSocketListener _listener;

		public MobileTcpListener (MobileNetworkSettings settings)
		{
			_settings = settings;
			_listener = new TcpSocketListener ();
		}

		#region ITcpClient implementation
		public Task ConnectAsync (string userId)
		{
			throw new NotImplementedException ();
		}

		public Task DisconnectAsync ()
		{
			throw new NotImplementedException ();
		}

		public System.IO.Stream ReadStream {
			get {
				throw new NotImplementedException ();
			}
		}

		public System.IO.Stream WriteStream {
			get {
				throw new NotImplementedException ();
			}
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

