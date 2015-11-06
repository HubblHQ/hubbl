using System;
using MessageRouter.Network;

namespace Hubl.Mobile
{
	public class MobileNetworkClientFactory: INetworkClientFactory
	{

		public MobileNetworkClientFactory ()
		{
		}

		#region INetworkClientFactory implementation

		public IMulticastClient CreateMulticastClient ()
		{
			throw new NotImplementedException ();
		}

		public ITcpListener CreateListener ()
		{
			throw new NotImplementedException ();
		}

		public ITcpClient CreateTcpClient ()
		{
			throw new NotImplementedException ();
		}

		#endregion

	}
}

