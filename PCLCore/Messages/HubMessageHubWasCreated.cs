using System;
using System.ServiceModel.Channels;
using Hubl.Core.Model;
using MessageRouter.Message;
using System.Runtime.Serialization;
using Hubl.Core.Messages;
using MessageRouter.Network;
using System.Collections.Generic;

namespace Hubl.Core
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

