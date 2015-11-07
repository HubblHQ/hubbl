using System.Runtime.Serialization;
using Hubl.Core.Model;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Hubl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class ResponsePlayingTrackMessage:IMessage
    {
        [DataMember]
        public Track Track { get; set; }
    }
}
