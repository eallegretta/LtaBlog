using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eaa.LtaBlog.Application.Models;
using Raven.Client.Linq;
using Eaa.LtaBlog.Application.Properties;
using Eaa.LtaBlog.Application.Core.Queries;
using AutoMapper;
using Eaa.LtaBlog.Application.Core.Entities;
using Eaa.LtaBlog.Application.Core.Commands.Posts;

namespace Eaa.LtaBlog.Application.Controllers
{
    public class PostsController : LtaController
    {
        //
        // GET: /Posts/

        public ActionResult List(int? page = 1)
        {
			int skip = Settings.Default.PostsPageSize * (page.Value - 1);

			RavenQueryStatistics stats;

			var posts = RavenSession.Query<Post>()
				.Statistics(out stats)
				.Take(Settings.Default.PostsPageSize)
				.OrderByDescending(x => x.CreatedAt)
				.ToList();

			var viewModel = new PostsViewModel(posts, page.Value, stats.TotalResults);

			foreach (var post in viewModel.Posts)
			{
				post.Author = RavenSession.GetPostAuthor(post.Post);
			}

            return View(viewModel);
        }


		public ActionResult Show(string id)
		{
			var post = RavenSession.Load<Post>(id);

			if (post == null)
			{
				throw new HttpException(404, "The post you are looking for does not exist");
			}

			var postModel = Mapper.Map<PostsViewModel.PostModel>(post);

			return View(postModel);
		}

		[ChildActionOnly]
		public ActionResult TagCloud()
		{
			return new EmptyResult();
		}

		[HttpPost]
		public ActionResult AddComment(CommentInputModel input)
		{
			if (ModelState.IsValid)
			{
				var addCommentCmd = Mapper.Map<AddCommentCommand>(input);

				CommandProcessor.Process(addCommentCmd);

				if (addCommentCmd.IsValid)
				{
					Notify("Your comment has been received!", NotifyType.Success, NotifyPosition.Top);

					return RedirectToAction("Show", new { id = input.PostId });
				}

				ModelState.AddValidationResults(addCommentCmd.ValidationResults());
			}
			
			return RedirectToAction("Show", new { id = input.PostId });
		}

    }
}
