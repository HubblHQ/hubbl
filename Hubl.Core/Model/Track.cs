using System.Runtime.Serialization;

namespace Hubl.Core.Model
{
    [DataContract]
    public class Track
    {
        [DataMember]
        public string Artist { get; set; }

        [DataMember]
        public string Name { get; set; }


    }
}
