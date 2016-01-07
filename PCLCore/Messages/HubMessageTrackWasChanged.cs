using Module.MessageRouter.Abstractions.Message;
using Hubl.Core.Model;
using System.Runtime.Serialization;
using Module.MessageRouter.Abstractions.Network;
using Hubl.Core.Messages;

namespace Hubl.Core.Messages
{
	[DataContract, Message(MessageGroups.Player)]
	public class HubMessageTrackReputationWasChanged:IMessage
	{
		[DataMember]
		public PlaylistEntry Entry { get; set;}

		[DataMember]
		public User User{ get; set; }
	}
}

