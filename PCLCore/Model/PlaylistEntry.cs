using System;
using System.Runtime.Serialization;


namespace Hubl.Core.Model
{
	[DataContract]
	public class PlaylistEntry
	{
		[DataMember]
		public Track Track { get; set; }

		[DataMember]
		public User User { get; set; }

		[DataMember]
		int LikesNum { get; set; }

		[DataMember]
		int DislikesNum { get; set; }

		[DataMember]
		double Priority { get; set; }
	}
}

