using System.Collections.Generic;

namespace StatefulT4Processor.DeploymentManager.Models
{
	public class IndexViewModel
	{
		public IEnumerable<Deployment> Deployments { get; set; }
	}
}