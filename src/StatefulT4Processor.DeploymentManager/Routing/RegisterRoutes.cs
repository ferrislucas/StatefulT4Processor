using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace StatefulT4Processor.DeploymentManager.Routing
{
	public class RegisterRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"DeploymentManager/{action}",
				new { controller = "DeploymentManager", action = "Index" }
				);
		}
	}
}