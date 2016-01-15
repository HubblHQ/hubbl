using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Module.MessageRouter.Abstractions.Message;

namespace Hubbl.Core.Messages
{

	public class SendFileMessage : IStreamingMessage, IMessage
	{
		public SendFileMessage(string filename, ulong streamLength, Stream stream)
		{
			Filename = filename;
			StreamLength = streamLength;
			Stream = stream;
		}

		[DataMember]
		public string Filename { get; set; }

		/// <summary>Длина стрима</summary>
		[DataMember]
		public ulong StreamLength { get; set; }

		/// <summary>Стрим который нужно передать</summary>
		[IgnoreDataMember]
		public Stream Stream { get; set; }
	}

}
