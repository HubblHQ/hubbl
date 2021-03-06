﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Hubbl.Core.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
	[DataContract]
	[Message(MessageGroups.Player)]
	public class HubMessageHubWasCreated:IMessage
	{
		[DataMember]
		public HubblUser User{get;set;}

		[DataMember]
		public List<Track> CurrentPlaylist {get; set; }
	}
}

