using System;
using MessageRouter.Network;
using Sockets.Plugin.Abstractions;
using Sockets.Plugin;
using System.IO;
using System.Threading.Tasks;
using Hubl.Core.Service;

namespace Hubl.Mobile
{
	public class MobileTcpClient : ITcpClient
	{

		private readonly UsersService _usersService;
		private readonly TcpSocketClient _client;

		public MobileTcpClient(UsersService usersService) : this(usersService, new TcpSocketClient())
		{
		}

		public MobileTcpClient(UsersService usersService, TcpSocketClient client)
		{
			_usersService = usersService;
			_client = client;
		}


		public void Dispose()
		{
			_client.Dispose();
		}

		public async Task ConnectAsync(string userId)
		{
			var user = _usersService.Get(userId);
			if (user == null)
				return;
			await _client.ConnectAsync(user.IpAddress, user.Port);
			ReadStream = _client.GetStream();
			WriteStream = ReadStream;
		}

		public Task DisconnectAsync()
		{
			return _client.DisconnectAsync ();
		}


		#region ITcpClient implementation

		public Task ConnectAsync (string userId)
		{
			return _client.ConnectAsync ();
		}

		public Task DisconnectAsync ()
		{
			return _client.DisconnectAsync ();
		}

		public Stream ReadStream {
			get {	
				return _client.ReadStream;
			}
		}

		public Stream WriteStream {
			get {
				return _client.WriteStream;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			_client.Dispose ();
		}

		#endregion

	}
}

