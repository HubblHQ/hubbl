using System.Runtime.Serialization;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
    [DataContract]
    [Message(MessageGroups.System)]
    public class HelloMessage : IMessage
    {
        public HelloMessage(HubblUser user)
        {
            Sender = user;
        }

        [DataMember]
        public HubblUser Sender { get; set; }
    }
}
