using System.Collections.Generic;
using StatefulT4Processor.TextTemplateZipProcessor.Services;

namespace StatefulT4Processor.TextTemplateZipProcessor
{
	namespace StatefulT4Processor.BatchProcessor
	{
		public class TextTemplateZipProcessor
		{
			private readonly IExtractZipToDirectoryService extractZipToDirectoryService;
			private readonly IProcessAndDeleteT4TemplatesService processAndDeleteT4TemplatesService;

			public TextTemplateZipProcessor(IExtractZipToDirectoryService extractZipToDirectoryService,
									IProcessAndDeleteT4TemplatesService processAndDeleteT4TemplatesService)
			{
				this.processAndDeleteT4TemplatesService = processAndDeleteT4TemplatesService;
				this.extractZipToDirectoryService = extractZipToDirectoryService;
			}

			public IEnumerable<string> ProcessZip(string pathToZip, string outputPath)
			{
				extractZipToDirectoryService.ExtractToPath(pathToZip, outputPath);

				var errors = processAndDeleteT4TemplatesService.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(outputPath);

				return errors;
			}
		}
	}
}
