using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;

namespace Hubl.Core.Model
{
    [DataContract]
    public class Track
    {
		[DataMember]
        public string Id { get; set; }

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

		[DataMember]
		public SourceType SourceType { get; set; }

		[DataMember]
		public string Genre { get; set; }

    }
}
