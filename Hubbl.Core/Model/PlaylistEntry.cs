using System;
using System.Runtime.Serialization;


namespace Hubbl.Core.Model
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
		public HubblUser User { get; set; }

		[DataMember]
		public int LikesNum { get; set; }

		[DataMember]
		public int DislikesNum { get; set; }

		[DataMember]
		public double Priority { get; set; }

        public override string ToString()
        {
            return "" + Id + ": " + Track + " +" + LikesNum + "|-" + DislikesNum;
        }
    }
}

