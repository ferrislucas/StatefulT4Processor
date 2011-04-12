using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.GitDeployment.Models
{
	public class GitDeploymentTarget
	{
		public string BranchName { get; set; }
		public string RepositoryUrl { get; set; }
	}
}
