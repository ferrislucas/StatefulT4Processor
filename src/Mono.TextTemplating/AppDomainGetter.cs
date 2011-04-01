using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mono.TextTemplating
{
	using System;
	using System.Reflection;
	using System.Runtime.Remoting;

	public class AppDomainGetter
	{
		public static AppDomain GetNewAppDomain(string name)
		{
			AppDomain ad = AppDomain.CreateDomain(name);
			return ad;
		}
	}

	public class ShowAppDomain : MarshalByRefObject
	{
		public string GetAppDomainName()
		{
			return AppDomain.CurrentDomain.FriendlyName; // gets the Current application domain thread and returns it's nameAppDomain name 
		}
	}

	public class CallMyHelloWorld
	{
		public static void Main()
		{
			AppDomain ad = AppDomain.CreateDomain("Yupee! My AppDomain!"); // create a new domain 
			ShowAppDomain ad2 = (ShowAppDomain)ad.CreateInstanceAndUnwrap(Assembly.GetCallingAssembly().GetName().Name, "ShowAppDomain");

			/* 
			We use the AppDomain.CreateInstanceAndUnwrap method to Create a new instance of a specified type. 
			Assembly.GetCallingAssembly().GetName().Name returns The Assembly object and the name of the method that calls the presently executing method. 
			*/

			Console.WriteLine("Here's my own AppDomain " + ad2.GetAppDomainName()); // Voila! 
		}
	}
}
