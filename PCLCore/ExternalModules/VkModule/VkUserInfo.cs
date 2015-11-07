using System;
using System.Security.Principal;

namespace PCLCore
{
	public class VkUserInfo
	{
		public readonly string _token;

		public readonly string VkUserId;

		public VkUserInfo (string _token, string uid)
		{
			this._token = _token;
			VkUserId = uid;
		}
	}
}

