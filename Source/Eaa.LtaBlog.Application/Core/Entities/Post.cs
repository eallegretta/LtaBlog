using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eaa.LtaBlog.Application.Core.Entities
{
	public class Post
	{
		public Post()
		{
			Tags = new List<string>();
			Comments = new List<Comment>();
		}

		public string Id { get; set; }

		public string AuthorId { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public DateTimeOffset? LastEditedAt { get; set; }

		public string LastEditedByUserId { get; set; }

		public bool AllowComments { get; set; }

		public bool IsActive { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public List<string> Tags { get; private set; }

		public List<Comment> Comments { get; private set; }
	}
}