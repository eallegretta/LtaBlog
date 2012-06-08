using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Eaa.LtaBlog.Application.Models
{
	public class CommentInputModel
	{
		[Required]
		public string PostId { get; set; }

		[Required]
		public string CommentText { get; set; }
	}
}
