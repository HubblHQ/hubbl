using System;
using System.Runtime.Serialization;
using Module.MessageRouter.Abstractions.Message;

namespace Hubl.Core.Messages
{
    [DataContract]
    public class PlayTrackMessage:IMessage
    {
        [DataMember]
        public Guid TrackId { get; set; }
    }
}
