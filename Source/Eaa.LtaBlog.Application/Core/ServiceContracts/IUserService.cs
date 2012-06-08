using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.Core.ServiceContracts
{
	public interface ILoggedUserService
	{
		User GetLoggedUser();
		void RefreshLoggedUserInformation();
	}
}