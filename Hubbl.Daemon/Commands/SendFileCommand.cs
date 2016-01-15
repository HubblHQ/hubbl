using System;
using System.Collections.Generic;
using System.IO;
using Hubbl.Core.Messages;
using Hubbl.Core.Service;
using Hubbl.Daemon.Properties;
using Module.MessageRouter.Abstractions.Network.Interfaces;

namespace Hubbl.Daemon.Commands
{
	internal class SendFileCommand : ICommand
	{
		private readonly INetworkMessageRouter _router;
		private readonly ISession _session;

		public SendFileCommand(INetworkMessageRouter router, ISession session)
		{
			_router = router;
			_session = session;
			Shortcuts = new[] { "sendfile" };
			Description = Resources.SendFileCommand;
		}

		public bool Execute(params string[] args)
		{
			var ids = args[0].Split(',');

			var file_location = args[1];

			var fileName = Path.GetFileName(file_location);

			var exists = File.Exists(file_location);

			if (!exists)
				Console.WriteLine(Resources.SendFileCommand_Execute_File_not_found);

			Stream stream = File.Open(file_location, FileMode.Open);

			
			var tasks = _router.PublishFor(ids, new SendFileMessage(fileName, (ulong)stream.Length, stream));

			//TODO: put this tasks to task-container (https://github.com/mesenev/hubbl/issues/3)

			return false;
		}

		public IEnumerable<string> Shortcuts { get; private set; }
		public string Description { get; private set; }
	}
}
