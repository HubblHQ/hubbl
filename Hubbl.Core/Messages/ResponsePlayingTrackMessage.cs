using System.Runtime.Serialization;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class ResponsePlayingTrackMessage:IMessage
    {
        [DataMember]
        public Track Track { get; set; }
    }
}
