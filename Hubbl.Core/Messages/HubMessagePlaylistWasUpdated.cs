using System;
using Module.MessageRouter.Abstractions.Message;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Hubbl.Core.Model;
using Hubbl.Core.Messages;
using Module.MessageRouter.Abstractions.Network;

namespace Hubbl.Core.Messages
{
	[DataContract]
	[Message(MessageGroups.Player)]
	public class HubMessagePlaylistWasUpdated:IMessage
	{
		[DataMember]
		public IEnumerable<PlaylistEntry> Playlist { get; set; }

		[DataMember]
		public HubblUser User { get; set;}

		[DataMember]
		public PlaylistEntry PlayingTrack { get; set;}

	}
}

