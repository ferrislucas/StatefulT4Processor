using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using StatefulT4Processor.BatchProcessor.Helpers;
using StatefulT4Processor.BatchProcessor.Models;
using StatefulT4Processor.BatchProcessor.Services;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.BatchProcessor
{
	public class ZipBatchProcessor_remove
	{
		private readonly IFileSystem fileSystem;
		private readonly IGuidGetter guidGetter;
		private readonly IGetWorkingFolderPath getWorkingFolderPath;
		private readonly IExtractZipToDirectoryService extractZipToDirectoryService;
		private readonly IProcessAndDeleteT4TemplatesService processAndDeleteT4TemplatesService;

		public ZipBatchProcessor_remove(IGuidGetter guidGetter, 
								IFileSystem fileSystem,
								IGetWorkingFolderPath getWorkingFolderPath,
								IExtractZipToDirectoryService extractZipToDirectoryService,
								IProcessAndDeleteT4TemplatesService processAndDeleteT4TemplatesService)
		{
			this.processAndDeleteT4TemplatesService = processAndDeleteT4TemplatesService;
			this.extractZipToDirectoryService = extractZipToDirectoryService;
			this.getWorkingFolderPath = getWorkingFolderPath;
			this.guidGetter = guidGetter;
			this.fileSystem = fileSystem;
		}

		public string ProcessBatch(ZipBatch zipBatch)
		{
			var pathToWorkingDirectory = getWorkingFolderPath.GetPathToWorkingFolder() + 
											"BatchProcessTemp" + 
											Path.DirectorySeparatorChar + 
											guidGetter.GetGuid();

			fileSystem.CreateFolder(pathToWorkingDirectory);

			var pathToZip = getWorkingFolderPath.GetPathToWorkingFolder() + 
										"BatchProcessFileUploads" + 
										Path.DirectorySeparatorChar + 
										zipBatch.Id +
										Path.DirectorySeparatorChar + zipBatch.ZipFilename;
			
			extractZipToDirectoryService.ExtractToPath(pathToZip, pathToWorkingDirectory);

			processAndDeleteT4TemplatesService.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(pathToWorkingDirectory);

			return pathToWorkingDirectory;
		}
	}
}
