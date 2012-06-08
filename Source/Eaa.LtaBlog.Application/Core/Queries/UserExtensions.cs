using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.Core.Queries
{
	public static class UserExtensions
	{
		public static User GetUserByEmail(this IDocumentSession session, string email)
		{
			return session.Query<User>().FirstOrDefault(x => x.Email == email);
		}
	}
}