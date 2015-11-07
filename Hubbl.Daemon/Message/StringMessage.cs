using System;
using System.Runtime.Serialization;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Hubl.Daemon.Message
{
	[DataContract]
	[Message("System")]
	public class StringMessage : IMessage
	{
		public StringMessage (String str) { 
			StrMessage = str; 
		}

		[DataMember]
		public String StrMessage { get; set; }
	}

}

