using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.GitDeployment.Models
{
	public class GitDeploymentResult
	{
		public IEnumerable<string> Errors { get; set; }
		public GitDeploymentStatus GitDeploymentStatus { get; set; }
	}

	public enum GitDeploymentStatus
	{
		Success,
		Failure
	}
}
