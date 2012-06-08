using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eaa.LtaBlog.Application.Core.Entities
{
	public class Comment
	{
		public string Id { get; set; }
		public string Text { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
	}
}