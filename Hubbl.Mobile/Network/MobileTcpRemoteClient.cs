using System.IO;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network;
using Sockets.Plugin.Abstractions;

namespace Hubbl.Mobile.Network
{
	public class MobileTcpRemoteClient:IRemoteClient
	{
	    private readonly ITcpSocketClient _client;

	    public MobileTcpRemoteClient (RemotePoint point, ITcpSocketClient client)
	    {
	        _client = client;
	        RemotePoint = point;
	        ReadStream = client.ReadStream;
	        WriteStream = client.WriteStream;

	    }

	    public void Dispose()
	    {
	        _client.Dispose();
	    }

	    public Stream ReadStream { get; private set; }
	    public Stream WriteStream { get; private set; }
	    public RemotePoint RemotePoint { get; private set; }
	    public Task DisconnectAsync()
	    {
	        return _client.DisconnectAsync();
	    }
	}
}

