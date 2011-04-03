using System.IO;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor;

namespace StatefulT4Processor.TextTemplateBatchManager.Services
{
	public class TextTemplateBatchProcessor
	{
		private readonly IFileSystem fileSystem;
		private readonly IGuidGetter guidGetter;
		private readonly IGetWorkingFolderPath getWorkingFolderPath;
		private readonly ITextTemplateZipProcessor textTemplateZipProcessor;

		public TextTemplateBatchProcessor(IGuidGetter guidGetter, 
								IFileSystem fileSystem,
								IGetWorkingFolderPath getWorkingFolderPath,
								ITextTemplateZipProcessor textTemplateZipProcessor)
		{
			this.textTemplateZipProcessor = textTemplateZipProcessor;
			this.getWorkingFolderPath = getWorkingFolderPath;
			this.guidGetter = guidGetter;
			this.fileSystem = fileSystem;
		}

		public string ProcessBatch(TextTemplateBatch textTemplateBatch)
		{
			var pathToExtractZipTo = getWorkingFolderPath.GetPathToWorkingFolder() + 
											"BatchProcessTemp" + 
											Path.DirectorySeparatorChar + 
											guidGetter.GetGuid();

			fileSystem.CreateFolder(pathToExtractZipTo);

			var pathToZip = getWorkingFolderPath.GetPathToWorkingFolder() + 
										"BatchProcessFileUploads" + 
										Path.DirectorySeparatorChar + 
										textTemplateBatch.Id +
										Path.DirectorySeparatorChar + textTemplateBatch.ZipFilename;

			textTemplateZipProcessor.ProcessZip(pathToZip, pathToExtractZipTo);

			return pathToExtractZipTo;
		}
	}
}
