using System.Collections.Generic;
using System.Runtime.Serialization;
using Hubl.Core.Model;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Hubl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class ResponsePlaylistMessage:IMessage
    {
        [DataMember]
        public IEnumerable<Track> Playlist { get; set; }
    }
}
