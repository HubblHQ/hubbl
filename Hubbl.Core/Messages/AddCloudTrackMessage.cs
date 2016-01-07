using System.Runtime.Serialization;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Network;
using Module.MessageRouter.Abstractions.Message;

namespace Hubbl.Core.Messages
{
	[DataContract, Message(MessageGroups.Player)]
	public class AddCloudTrackMessage : IMessage
	{
		[DataMember]
		public Track Track { get; set; }

		[DataMember]
		public HubblUser Sender { get; set; }
	}
}
