using System.Runtime.Serialization;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Core.Messages
{
    [DataContract, Message(MessageGroups.Other)]
    public class TextMessage : IMessage
    {
        public TextMessage(string text)
        {
            Text = text;
        }

        [DataMember]
        public string Text { get; set; }
    }
}
