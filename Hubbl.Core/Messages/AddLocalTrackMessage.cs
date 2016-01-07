using System.IO;
using System.Runtime.Serialization;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class AddLocalTrackMessage : IStreamingMessage
    {
        [DataMember]
        public Track Track { get; set; }

        [DataMember]
        public ulong StreamLength { get; set; }

        [IgnoreDataMember]
        public Stream Stream { get; set; }
    }
}
