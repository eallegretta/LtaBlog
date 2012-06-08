using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Eaa.LtaBlog.Application.Models;

namespace Eaa.LtaBlog.Application.Core.Entities.Security
{
	public class LtaPrincipal: IPrincipal
	{
		private User _user;

		public LtaPrincipal(User user)
		{
			_user = user;
		}

		#region IPrincipal Members
	

		public IIdentity Identity
		{
			get { return _user; }
		}

		public bool IsInRole(string role)
		{
			return _user.Roles.Contains(role);
		}

		#endregion
	}
}