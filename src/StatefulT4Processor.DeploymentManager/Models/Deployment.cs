﻿using System;

namespace StatefulT4Processor.DeploymentManager.Models
{
	[Serializable]
	public class Deployment
	{
		public string Id { get; set; }
		public string Name { get; set;}
	}
}