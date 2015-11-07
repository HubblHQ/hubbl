using System.Runtime.Serialization;
using Hubl.Core.Model;
using MessageRouter.Message;
using MessageRouter.Network;

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
