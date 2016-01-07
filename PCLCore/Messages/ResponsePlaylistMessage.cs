using System.Collections.Generic;
using System.Runtime.Serialization;
using Hubl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class ResponsePlaylistMessage:IMessage
    {
        [DataMember]
        public IEnumerable<Track> Playlist { get; set; }

		[DataMember]
		public Track PlayingTrack { get; set;}
    }
}
