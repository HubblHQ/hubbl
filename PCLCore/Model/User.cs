using System.Runtime.Serialization;
using PCLCore;
using System.Globalization;
using System;

namespace Hubl.Core.Model
{
    public class User
    {
        public int Port { get; set; }

        public string IpAddress { get; set; }

        public string Title { get; set; }

        public string Id { get; set; }

		[DataMember]
		public VkUserInfo VkUserInfo { get; set;}

		[DataMember]
		public SCUserInfo SoundCloud { get; set; }
    }
}
