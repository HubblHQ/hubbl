using System.Runtime.Serialization;
using PCLCore;

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

		[DataMember]
		public VkUserInfo VkUserInfo { get; set;}

		[DataMember]
		public SCUserInfo SoundCloud { get; set; }
    }
}
