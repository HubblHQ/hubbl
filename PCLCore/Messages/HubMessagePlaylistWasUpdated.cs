using System;
using Module.MessageRouter.Abstractions.Message;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Hubl.Core.Model;
using Hubl.Core.Messages;
using Module.MessageRouter.Abstractions.Network;

namespace Hubl.Core.Messages
{
	[DataContract]
	[Message(MessageGroups.Player)]
	public class HubMessagePlaylistWasUpdated:IMessage
	{
		[DataMember]
		public IEnumerable<PlaylistEntry> Playlist { get; set; }

		[DataMember]
		public User User { get; set;}

		[DataMember]
		public PlaylistEntry PlayingTrack { get; set;}

	}
}

