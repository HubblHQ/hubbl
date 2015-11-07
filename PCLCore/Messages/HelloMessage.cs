using System.Runtime.Serialization;
using Hubl.Core.Model;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Hubl.Core.Messages
{
    [DataContract]
    [Message(MessageGroups.System)]
    public class HelloMessage : IMessage
    {
        [DataMember]
        public User Sender { get; set; }
    }
}
