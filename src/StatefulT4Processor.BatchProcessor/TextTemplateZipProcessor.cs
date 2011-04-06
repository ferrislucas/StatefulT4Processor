using System.Collections.Generic;
using StatefulT4Processor.TextTemplateZipProcessor.Services;

namespace StatefulT4Processor.TextTemplateZipProcessor
{
	namespace StatefulT4Processor.BatchProcessor
	{
		public interface ITextTemplateZipProcessor
		{
			IEnumerable<string> ProcessZip(string pathToZip, string outputPath);
		}

		public class TextTemplateZipProcessor : ITextTemplateZipProcessor
		{
			private readonly IExtractZipToDirectoryService extractZipToDirectoryService;
			private readonly IProcessAndDeleteT4TemplatesService processAndDeleteT4TemplatesService;
			private readonly IRecursivelyRenameFilesAndFoldersByConvention recursivelyRenameFilesAndFoldersByConvention;

			public TextTemplateZipProcessor(IExtractZipToDirectoryService extractZipToDirectoryService,
									IProcessAndDeleteT4TemplatesService processAndDeleteT4TemplatesService,
									IRecursivelyRenameFilesAndFoldersByConvention recursivelyRenameFilesAndFoldersByConvention)
			{
				this.recursivelyRenameFilesAndFoldersByConvention = recursivelyRenameFilesAndFoldersByConvention;
				this.processAndDeleteT4TemplatesService = processAndDeleteT4TemplatesService;
				this.extractZipToDirectoryService = extractZipToDirectoryService;
			}

			public IEnumerable<string> ProcessZip(string pathToZip, string outputPath)
			{
				extractZipToDirectoryService.ExtractToPath(pathToZip, outputPath);

				var errors = processAndDeleteT4TemplatesService.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(outputPath);

				recursivelyRenameFilesAndFoldersByConvention.RecursivelyRename(outputPath);				

				return errors;
			}
		}
	}
}
