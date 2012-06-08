using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.Properties;
using System.Web.Mvc;
using AutoMapper;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.Models
{
	public class PostsViewModel
	{
		private readonly int _totalCount;

		public PostsViewModel(IList<Post> posts, int currentPage, int totalCount)
		{
			CurrentPage = currentPage;
			_totalCount = totalCount;

			Posts = Mapper.Map(posts, Posts);
		}

		public int CurrentPage
		{
			get;
			private set;
		}

		public bool HasPrevPage
		{
			get
			{
				return CurrentPage * Settings.Default.PostsPageSize > Settings.Default.PostsPageSize;
			}
		}

		public bool HasNextPage
		{
			get { return CurrentPage * Settings.Default.PostsPageSize < _totalCount; }
		}

		public IEnumerable<PostModel> Posts { get; private set; }

		public class PostModel
		{
			public Post Post { get; set; }
			public int CommentsCount { get { return Post.Comments.Count; } }
			public User Author { get; set; }
		}


	}
}