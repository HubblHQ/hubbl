using System.Collections.Generic;
using System.Runtime.Serialization;
using Hubl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Core.Messages
{
	[DataContract]
	[Message(MessageGroups.Player)]
	public class HubMessageHubWasCreated:IMessage
	{
		[DataMember]
		public User User{get;set;}

		[DataMember]
		public List<Track> CurrentPlaylist {get; set; }
	}
}

