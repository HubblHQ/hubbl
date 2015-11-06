using System;
using System.Runtime.Serialization;

namespace Hubl.Core.Model
{
    [DataContract]
    public class Track
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Artist { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TimeSpan Duration { get; set; }

        [DataMember]
        public TimeSpan Current { get; set; }

		[DataMember]
		public string Source { get; set; }
    }
}
