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
			MulticastAdress = "224.0.0.1";
			Adapters = new CommsInterface ();
		}
	}
}

