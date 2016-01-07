using Module.MessageRouter.Abstractions.Message;
using System.Runtime.Serialization;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Network;
using Hubbl.Core.Messages;

namespace Hubbl.Core.Messages
{
	[DataContract, Message(MessageGroups.Player)]
	public class HubMessageTrackReputationWasChanged:IMessage
	{
		[DataMember]
		public PlaylistEntry Entry { get; set;}

		[DataMember]
		public HubblUser User{ get; set; }
	}
}

