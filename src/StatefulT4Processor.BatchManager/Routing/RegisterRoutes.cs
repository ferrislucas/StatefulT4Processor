using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace StatefulT4Processor.TextTemplateBatchManager.Routing
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
				"TextTemplateBatchManager/{action}",
				new { controller = "TextTemplateBatchManager", action = "Index" }
				);

			routes.MapRoute(
				null,
				"",
				new { controller = "TextTemplateBatchManager", action = "Index" }
				);
		}
	}
}