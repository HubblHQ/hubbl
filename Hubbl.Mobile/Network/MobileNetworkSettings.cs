using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Hubbl.Mobile.Network
{
	public class MobileNetworkSettings
	{
		public int TTL {get;set;}

		public int ListenPort  { get; set;}

		public string MulticastAdress { get; set;}
		public ICommsInterface Adapters { get; set; }
		public int MulticastPort { get; set;}


		public MobileNetworkSettings ()
		{
			TTL = 10;
			MulticastPort = 30307;
			ListenPort = 30303;
			MulticastAdress = "239.0.0.222";
			Adapters = null;
			var interfaces = Sockets.Plugin.CommsInterface.GetAllInterfacesAsync ().Result;
			foreach (var i in interfaces) {
				if (i.IsUsable && !i.IsLoopback) {
					//Adapters = i;
					break;
				}			
			}
		}
	}
}

