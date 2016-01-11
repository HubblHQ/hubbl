using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Mobile.Network
{
	public class MobileNetworkClientFactory: INetworkClientFactory
	{

		private readonly MobileNetworkSettings _networkSettings;
		private readonly UsersService<HubblUser> _usersService;

		public MobileNetworkClientFactory(MobileNetworkSettings networkSettings,
			UsersService<HubblUser> usersService)
		{
			_networkSettings = networkSettings;
			_usersService = usersService;
		}

		#region INetworkClientFactory implementation

		public IMulticastClient CreateMulticastClient ()
		{
			return new MobileMulticastClient (_networkSettings);
		}

		public ITcpListener CreateListener ()
		{
			return new MobileTcpListener (_networkSettings, _usersService);
		}

		public ITcpClient CreateTcpClient ()
		{
			return new MobileTcpClient (_usersService);
		}

		#endregion

	}
}

