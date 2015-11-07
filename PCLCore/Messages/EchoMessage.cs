using System.Runtime.Serialization;
using MessageRouter.Message;
using MessageRouter.Network;

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
