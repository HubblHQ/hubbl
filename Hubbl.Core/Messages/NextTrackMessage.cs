using System.Runtime.Serialization;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class NextTrackMessage:IMessage
    {
    }
}
