using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;

namespace StatefulT4Processor.Webroot
{
	public class MvcApplication : TurbineApplication
	{

		public MvcApplication()
		{
			var locator = new UnityServiceLocator();
			locator.Register<IServiceLocator>(locator);
			ServiceLocatorManager.SetLocatorProvider(() => locator);
		}
	}
}