using System.IO;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;
using StatefulT4Processor.TextTemplateBatchManager.Models;

namespace StatefulT4Processor.TextTemplateBatchManager.Services
{
	public class ZipBatchProcessor_remove
	{
		private readonly IFileSystem fileSystem;
		private readonly IGuidGetter guidGetter;
		private readonly IGetWorkingFolderPath getWorkingFolderPath;

		public ZipBatchProcessor_remove(IGuidGetter guidGetter, 
								IFileSystem fileSystem,
								IGetWorkingFolderPath getWorkingFolderPath)
		{
			this.getWorkingFolderPath = getWorkingFolderPath;
			this.guidGetter = guidGetter;
			this.fileSystem = fileSystem;
		}

		public string ProcessBatch(TextTemplateBatch textTemplateBatch)
		{
			var pathToWorkingDirectory = getWorkingFolderPath.GetPathToWorkingFolder() + 
											"BatchProcessTemp" + 
											Path.DirectorySeparatorChar + 
											guidGetter.GetGuid();

			fileSystem.CreateFolder(pathToWorkingDirectory);

			var pathToZip = getWorkingFolderPath.GetPathToWorkingFolder() + 
										"BatchProcessFileUploads" + 
										Path.DirectorySeparatorChar + 
										textTemplateBatch.Id +
										Path.DirectorySeparatorChar + textTemplateBatch.ZipFilename;			

			return pathToWorkingDirectory;
		}
	}
}
