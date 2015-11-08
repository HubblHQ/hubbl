using System;
using MessageRouter.Message;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Hubl.Core.Model;
using System.ServiceModel.Channels;
using Hubl.Core.Messages;

namespace Hubl.Core
{
	[DataContract, Message(MessageGroups.Player)]
	public class HubMessagePlaylistWasUpdated:IMessage
	{
		[DataMember]
		public IEnumerable<Track> Playlist { get; set; }

		[DataMember]
		public Track PlayingTrack { get; set;}
	}
}

