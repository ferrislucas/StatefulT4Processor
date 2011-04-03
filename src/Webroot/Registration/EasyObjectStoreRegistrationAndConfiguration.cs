using System.Collections.Generic;
using System.Configuration;
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
			return ConfigurationManager.AppSettings["PathToLocalWorkingFolder"] + @"Data\";
		}

		public void Register(IServiceLocator locator)
		{
			locator.Register<IGetPathToDataDirectoryService, GetPathToDataDirectoryService>();
		}
	}
}