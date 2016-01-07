using System;
using System.Security.Principal;
using System.Runtime.Serialization;

namespace Hubbl.Core
{
	[DataContract]
	public class VkUserInfo
	{
		[DataMember]
		public readonly string _token;

		[DataMember]
		public readonly string VkUserId;

		public VkUserInfo (string _token, string uid)
		{
			this._token = _token;
			VkUserId = uid;
		}
	}
}

