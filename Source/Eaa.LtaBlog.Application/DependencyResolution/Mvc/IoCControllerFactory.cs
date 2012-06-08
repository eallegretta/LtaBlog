using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;

namespace Eaa.LtaBlog.Application.DependencyResolution.Mvc
{
	public class IoCControllerFactory: System.Web.Mvc.DefaultControllerFactory
	{
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
				return null;

			if (!typeof(IController).IsAssignableFrom(controllerType))
				throw new CantCreateControllerException(controllerType, requestContext);

			return IoC.Container.GetInstance(controllerType) as IController;
		}

		public override void ReleaseController(IController controller)
		{
			controller = null;
		}

		public class CantCreateControllerException: Exception
		{
			private string _message;

			public CantCreateControllerException(Type controllerType, RequestContext requestContext)
			{
				var errorMsg = new StringBuilder();
				errorMsg.Append("Cannot create an instance of the requested controller specified by the route: ");
				errorMsg.AppendFormat("{0}/{1}/{2}", requestContext.RouteData.Values["area"],
					requestContext.RouteData.Values["controller"],
					requestContext.RouteData.Values["action"]);
				_message = errorMsg.ToString();
			}

			public override string Message
			{
				get { return _message; }
			}
		}
	}
}