using System.Runtime.Serialization;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
    [DataContract, Message(MessageGroups.Other)]
    public class SnapshotMessage : IMessage
    {
        public SnapshotMessage(ServerSnapshot snapshot)
        {
            Snapshot = snapshot;
        }

        [DataMember]
        public ServerSnapshot Snapshot { get; set; }
    }
}