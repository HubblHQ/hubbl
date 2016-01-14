using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Module.MessageRouter.Abstractions.Message;

namespace Hubbl.Core.Messages
{

	public class FileTransferMessage : IStreamingMessage, IMessage
	{
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
