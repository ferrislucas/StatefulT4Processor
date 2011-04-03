using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.BatchProcessor.Helpers
{
	public interface IGetWorkingFolderPath
	{
		string GetPathToWorkingFolder();
	}

	public class GetWorkingFolderPath : IGetWorkingFolderPath
	{
		public string GetPathToWorkingFolder()
		{
			return ConfigurationManager.AppSettings["pathToLocalWorkingFolder"];
		}
	}
}
