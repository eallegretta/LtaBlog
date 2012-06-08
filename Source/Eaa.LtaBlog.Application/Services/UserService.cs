using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.Core.ServiceContracts;
using Eaa.LtaBlog.Application.Infraestructure.Caching;
using Raven.Client;
using Eaa.LtaBlog.Application.Core.Queries;
using System.Security.Principal;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.Services
{
	[RequiresCaching]
	public class LoggedUserService: ILoggedUserService
	{
		private readonly IDocumentSession _documentSession;

		public LoggedUserService(IDocumentSession documentSession)
		{
			_documentSession = documentSession;
		}

		#region ILoggedUserService Members

		[CacheOutput]
		public User GetLoggedUser()
		{
			IPrincipal principal;

			if (HttpContext.Current == null)
				principal = System.Threading.Thread.CurrentPrincipal;
			else
				principal = HttpContext.Current.User;

			if (!principal.Identity.IsAuthenticated)
				return null;

			return _documentSession.GetUserByEmail(principal.Identity.Name);
		}

		public void RefreshLoggedUserInformation()
		{
			var cacheKeyProvider = new BasicCacheKeyProvider();

			string cacheKey = cacheKeyProvider.GetCacheKeyForMethod(this, this.GetType().GetMethod("GetLoggedUser"), null);

			CacheProviderFactory.Default.GetCacheProvider().Remove(cacheKey);
		}

		#endregion
	}
}