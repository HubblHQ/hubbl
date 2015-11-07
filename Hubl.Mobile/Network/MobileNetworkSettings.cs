using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Hubl.Mobile.Network
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
			TTL = 5;
			MulticastPort = 30307;
			ListenPort = 30308;
			MulticastAdress = "224.0.0.1";
			Adapters = null;
			var interfaces = Sockets.Plugin.CommsInterface.GetAllInterfacesAsync ().Result;
			foreach (var i in interfaces) {
				var ss = 3;
			}			
		}
	}
}

