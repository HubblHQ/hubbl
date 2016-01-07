using System.Runtime.Serialization;
using Hubl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class ResponsePlayingTrackMessage:IMessage
    {
        [DataMember]
        public Track Track { get; set; }
    }
}
