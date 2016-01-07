using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
    [DataContract, Message(MessageGroups.System)]
    public class EchoMessage:IMessage
    {
        public EchoMessage(HubblUser sender)
        {
            Sender = sender;
        }

        [DataMember]
        public HubblUser Sender { get; set; }
    }
}
