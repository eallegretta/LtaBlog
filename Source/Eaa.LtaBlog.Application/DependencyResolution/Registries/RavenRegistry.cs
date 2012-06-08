using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using Raven.Client;
using Eaa.LtaBlog.Application.RavenDb;

namespace Eaa.LtaBlog.Application.DependencyResolution.Registries
{
	public class RavenRegistry : Registry
	{
		public RavenRegistry()
		{
			For<IDocumentStore>()
				.Singleton()
				.Use(x => RavenDbStore.DocumentStore);

			For<IDocumentSession>()
				.HybridHttpOrThreadLocalScoped()
				.Use(x => RavenDbStore.DocumentStore.OpenSession());

			For<Lazy<IDocumentSession>>()
				.Singleton()
				.Use(x => new Lazy<IDocumentSession>(() => x.GetInstance<IDocumentSession>(), false));
		}
	}
}