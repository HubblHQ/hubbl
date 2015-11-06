using System.Runtime.Serialization;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Hubl.Core.Messages
{
    [DataContract, Message(MessageGroups.Player)]
    public class NextTrackMessage:IMessage
    {
    }
}
