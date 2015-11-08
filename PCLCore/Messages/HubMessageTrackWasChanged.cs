using MessageRouter.Message;
using Hubl.Core.Model;
using System.Runtime.Serialization;
using MessageRouter.Network;
using Hubl.Core.Messages;

namespace Hubl.Core.Messages
{
	[DataContract, Message(MessageGroups.Player)]
	public class HubMessageTrackReputationWasChanged:IMessage
	{
		[DataMember]
		public PlaylistEntry entry;
	}
}

