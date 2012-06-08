using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.RavenDb.Indexes;
using Eaa.LtaBlog.Application.Core.Entities;
using AutoMapper;

namespace Eaa.LtaBlog.Application.Models
{
	public class TagCloudViewModel
	{
		public TagCloudViewModel(IList<Tag> tags)
		{
			Tags = Mapper.Map<IList<TagModel>>(tags);
		}

		public IList<TagModel> Tags { get; private set; }

		public class TagModel
		{
			public string Name { get; set; }
			public int Count { get; set; }
		}
	}
}