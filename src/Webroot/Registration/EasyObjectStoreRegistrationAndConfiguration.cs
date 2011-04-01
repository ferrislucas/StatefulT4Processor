using System.Collections.Generic;
using System.Reflection;
using EasyObjectStore.Helpers;
using MvcTurbine.ComponentModel;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.Webroot.Registration
{
	public class EasyObjectStoreRegistrationAndConfiguration: InterfaceToSingleImplementationRegistrationConvention
    {
        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] { Assembly.GetAssembly(typeof(EasyObjectStore.Helpers.FileSystem)) };
        }
    }

	public class GetPathToDataDirectoryService : IGetPathToDataDirectoryService, IServiceRegistration
	{
		public string GetPathToDirectory()
		{
			return @"C:\_Application\StatefulT4Processor\localWorkingFolder\Data\";
		}

		public void Register(IServiceLocator locator)
		{
			locator.Register<IGetPathToDataDirectoryService, GetPathToDataDirectoryService>();
		}
	}
}