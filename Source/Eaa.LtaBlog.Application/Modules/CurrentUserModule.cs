using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.RavenDb;
using Eaa.LtaBlog.Application.Models;
using Eaa.LtaBlog.Application.Core.Entities.Security;
using Eaa.LtaBlog.Application.Core.Queries;
using Raven.Client;

namespace Eaa.LtaBlog.Application.Modules
{
	public class CurrentUserModule: IHttpModule
	{
		#region IHttpModule Members

		private Lazy<IDocumentSession> _session;

		/// <summary>
		/// Creates a new Current User Http Module
		/// </summary>
		/// <param name="session">A lazy IDocumentSession instace, since the registration of the module is before a session is created
		/// we need to lazily call it so it can be initialized properly</param>
		public CurrentUserModule(Lazy<IDocumentSession> session)
		{
			_session = session;
		}
		
		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += (sender, args) =>
				{
					var httpContext = HttpContext.Current;

					if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
					{
						var user = _session.Value.GetUserByEmail(context.User.Identity.Name);

						if (user != null)
						{
							httpContext.User = new LtaPrincipal(user);
						}
					}
				};
		}


		#endregion
	}
}