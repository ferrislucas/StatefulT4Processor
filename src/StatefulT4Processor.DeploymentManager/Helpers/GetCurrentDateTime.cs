using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatefulT4Processor.DeploymentManager.Helpers
{
	public interface IGetCurrentDateTime
	{
		DateTime Now();
	}

	public class GetCurrentDateTime : IGetCurrentDateTime
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}
	}
}