using System.Configuration;

namespace StatefulT4Processor.TextTemplateBatchManager.Helpers
{
	public interface IGetWorkingFolderPath
	{
		string GetPathToWorkingFolder();
	}

	public class GetWorkingFolderPath : IGetWorkingFolderPath
	{
		public string GetPathToWorkingFolder()
		{
			return ConfigurationManager.AppSettings["PathToLocalWorkingFolder"];
		}
	}
}
