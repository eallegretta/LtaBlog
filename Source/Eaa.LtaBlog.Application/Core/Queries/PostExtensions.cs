using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.Core.Queries
{
	public static class PostExtensions
	{
		public static User GetPostAuthor(this IDocumentSession session, Post post)
		{
			var user = session.Load<User>(post.AuthorId);

			return user;
		}
	}
}