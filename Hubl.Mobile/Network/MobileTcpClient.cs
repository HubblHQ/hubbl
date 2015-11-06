using System;
using MessageRouter.Network;
using Sockets.Plugin.Abstractions;
using Sockets.Plugin;
using System.IO;
using System.Threading.Tasks;

namespace Hubl.Mobile
{
	public class MobileTcpClient : ITcpClient
	{

		private readonly UsersService _usersService;
		private readonly ITcpClient _client;

		public MobileTcpClient(UsersService usersService) : this(usersService, new TcpClient())
		{
		}

		public MobileTcpClient(UsersService usersService, TcpClient client)
		{
			_usersService = usersService;
			_client = client;
		}


		public void Dispose()
		{
			_client.Dispose();
		}

		public Stream ReadStream { get; private set; }

		public Stream WriteStream { get; private set; }

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
			return Task.Run(() => _client.Close());
		}


		#region ITcpClient implementation

		public System.Threading.Tasks.Task ConnectAsync (string userId)
		{
			throw new NotImplementedException ();
		}

		public System.Threading.Tasks.Task DisconnectAsync ()
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
			throw new NotImplementedException ();
		}

		#endregion

		public TcpClient ()
		{
		}
	}
}

