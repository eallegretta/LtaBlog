using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.RavenDb.Indexes
{
	public class TagCloudIndex: Raven.Client.Indexes.AbstractIndexCreationTask<Post, Tag>
	{
		public TagCloudIndex()
		{
			Map = posts => from p in posts
						   from tag in p.Tags
						   select new { Name = tag, Count = 1 };


			Reduce = results => from r in results
								group r by r.Name into g
								select new { Name = g.Key, Count = g.Sum(x => x.Count) };
		}
	}
}