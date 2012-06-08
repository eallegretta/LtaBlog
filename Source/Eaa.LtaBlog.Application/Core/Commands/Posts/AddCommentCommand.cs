using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.Core.Commands.Posts
{
	public class AddCommentCommand: Command
	{
		[Required]
		public string PostId { get; set; }

		[Required]
		public string CommentText { get; set; }

		protected override bool Validate()
		{
			bool isValid = base.Validate();

			if (isValid)
			{
				var postExists = Session.Load<Post>(PostId) != null;

				if (!postExists)
				{
					AddValidationResult("The post you are trying to comment does not exist", "PostId");
					return false;
				}
			}

			return isValid;
		}

		public override void Execute()
		{
			var post = Session.Load<Post>(PostId);

			post.Comments.Add(new Comment { CreatedAt = DateTime.Now, Text = CommentText });

			Session.Store(post);
		}
	}
}