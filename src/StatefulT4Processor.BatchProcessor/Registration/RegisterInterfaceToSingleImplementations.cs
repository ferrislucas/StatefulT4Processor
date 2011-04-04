using System.Collections.Generic;
using System.Reflection;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.TextTemplateZipProcessor.Registration
{
	public class RegisterInterfaceToSingleImplementations : InterfaceToSingleImplementationRegistrationConvention
    {
        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}