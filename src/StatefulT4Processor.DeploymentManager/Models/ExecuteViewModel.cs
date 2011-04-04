using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatefulT4Processor.DeploymentManager.Models
{
	public class ExecuteViewModel
	{
		public IEnumerable<string> Errors { get; set; }
	}
}