using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using System.Configuration;
using Raven.Client.Indexes;
using Eaa.LtaBlog.Application.DependencyResolution;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.RavenDb
{
	public static class RavenDbStore
	{
		public static IDocumentStore DocumentStore;

		public static void Initialize()
		{
			var documentStore = new DocumentStore { ConnectionStringName = ConfigurationManager.ConnectionStrings[0].Name };

			Initialize(documentStore);
		}

		/// <summary>
		/// Initializes a new document store. This method is usefull for testing in order to send an already created In memory data store
		/// </summary>
		/// <param name="documentStore"></param>
		public static void Initialize(IDocumentStore documentStore)
		{
			documentStore.Initialize();
			documentStore.Conventions.IdentityPartsSeparator = "-";

			IndexCreation.CreateIndexes(typeof(RavenDbStore).Assembly, documentStore);

			DocumentStore = documentStore;

			StoreInitialData();
		}

		private static void StoreInitialData()
		{
			using (var session = DocumentStore.OpenSession())
			{
				var adminUser = CreateAdminUser(session);

				CreateInitialPost(adminUser, session);

				session.SaveChanges();
			}
		}

		public static IDocumentSession CurrentSession
		{
			get { return IoC.Container.GetInstance<IDocumentSession>(); }
		}

		private static void CreateInitialPost(User adminUser, IDocumentSession session)
		{
			bool hasPosts = session.Query<Post>().Any();

			if (!hasPosts)
			{
				var welcomePost = new Post
				{
					Title = "Welcome to your new blog!!!",
					Body = "Hi, LTA Blog would like to congratulate you for creating your new blog site!!!",
					AllowComments = false,
					AuthorId = adminUser.Id,
					CreatedAt = DateTimeOffset.Now,
					IsActive = true
				};
				welcomePost.Tags.Add("General");

				session.Store(welcomePost);
			}
		}

		private static User CreateAdminUser(IDocumentSession session)
		{
			var user = session.Query<User>().FirstOrDefault(u => u.Email.Equals("admin@ltablog.com"));

			if (user == null)
			{
				user = new User { Firstname = "John", Lastname = "Carter", Email = "admin@ltablog.com" };

				user.SetPassword("admin");

				session.Store(user);
			}

			return user;
		}
	}
}