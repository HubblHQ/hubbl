using System;

namespace MessageRouter
{
	public class SecurityAccessDeniedException : Exception
	{
		public string ErrorMessage { get; set;}

		public SecurityAccessDeniedException (string msg)
		{
			ErrorMessage = msg;
		}
	
	}
}

