using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eaa.LtaBlog.Application.Core.Commands
{
	public interface ICommandProcessor
	{
		void Process(Command command);
		TResult Process<TResult>(Command<TResult> command);
	}
}
