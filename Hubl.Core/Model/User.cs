using System.Runtime.Serialization;

namespace Hubl.Core.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public string IpAddress { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Id { get; set; }


    }
}
