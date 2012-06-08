using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eaa.LtaBlog.Application.RavenDb.Web
{
	public class RavenDbGlobalActionFilter: IActionFilter
	{
		#region IActionFilter Members

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.Exception != null)
				return; // don't commit changes if an exception was thrown

			using (RavenDbStore.CurrentSession)
				RavenDbStore.CurrentSession.SaveChanges();
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		#endregion
	}
}