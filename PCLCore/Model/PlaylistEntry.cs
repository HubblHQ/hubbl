using System;
using System.Runtime.Serialization;


namespace Hubl.Core.Model
{
	[DataContract]
	public class PlaylistEntry
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public Track Track { get; set; }

        [DataMember]
	    public bool IsCached { get; set; }

		[DataMember]
		public User User { get; set; }

		[DataMember]
		public int LikesNum { get; set; }

		[DataMember]
		public int DislikesNum { get; set; }

		[DataMember]
		public double Priority { get; set; }
	}
}

