using System;

namespace StatefulT4Processor.DeploymentManager.Models
{
	[Serializable]
	public class Deployment
	{
		public string Id { get; set; }
		public string Name { get; set;}
		public string StateXml { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? LastModifyDate { get; set; }
	}
}