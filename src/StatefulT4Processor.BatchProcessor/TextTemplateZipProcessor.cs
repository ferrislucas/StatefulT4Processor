using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.BatchProcessor.Helpers;
using StatefulT4Processor.BatchProcessor.Services;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.BatchProcessor
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;

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
