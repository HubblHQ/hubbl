using System.Runtime.Serialization;
using Module.MessageRouter.Abstractions.Network;
using Hubl.Core.Model;
using Module.MessageRouter.Abstractions.Message;

namespace Hubl.Core.Messages
{
	[DataContract, Message(MessageGroups.Player)]
	public class AddCloudTrackMessage : IMessage
	{
		[DataMember]
		public Track Track { get; set; }

		[DataMember]
		public User Sender { get; set; }
	}
}
