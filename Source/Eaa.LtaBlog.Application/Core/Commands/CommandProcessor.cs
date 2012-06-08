using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;

namespace Eaa.LtaBlog.Application.Core.Commands
{
	public class CommandProcessor: ICommandProcessor
	{
		#region ICommandProcessor Members
		private readonly IDocumentSession _session;


		public CommandProcessor(IDocumentSession session)
		{
			_session = session;
		}

		public void Process(Command command)
		{
			command.Session = _session;
			if (command.IsValid)
				command.Execute();
		}

		public TResult Process<TResult>(Command<TResult> command)
		{
			Process((Command)command);
			return command.Result;
		}

		#endregion
	}
}
