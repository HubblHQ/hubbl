using System.Runtime.Serialization;
using Hubl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Core.Messages
{
    [DataContract]
    [Message(MessageGroups.System)]
    public class HelloMessage : IMessage
    {
        public HelloMessage(User user)
        {
            Sender = user;
        }

        [DataMember]
        public User Sender { get; set; }
    }
}
