using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CATALYST.Core
{
	public interface IConfigurationState
	{
		string GetBaseTemplatePath();
		string GetBaseOutputPath();
	}

	public class ConfigurationState : IConfigurationState
	{
		public string GetBaseTemplatePath()
		{
			return "C:\\_APPLICATION\\CATALYST\\CATALYST\\CATALYST.Bootstrap\\Templates";
		}

		public string GetBaseOutputPath()
		{
			return "C:\\_APPLICATION\\CATALYST\\CATALYST\\CATALYST.Bootstrap\\Output";
		}
	}
}
