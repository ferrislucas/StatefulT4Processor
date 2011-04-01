using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace StatefulT4Processor.Webroot.Routing
{
	public class RegisterRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			//routes.MapRoute(
			//    null,
			//    "",
			//    new { controller = "T4StateManagement", action = "Index" }
			//    );

			routes.MapRoute(
				null,
				"T4StateManagement/{action}",
				new { controller = "T4StateManagement", action = "Index" }
				);
		}
	}
}