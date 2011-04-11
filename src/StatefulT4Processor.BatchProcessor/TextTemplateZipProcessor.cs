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
			private readonly IRecursivelyRenameFilesAndFoldersByConvention recursivelyRenameFilesAndFoldersByConvention;
			private readonly ICreateQueueFromPathService createQueueFromPathService;
			private readonly IQueueProcessorService queueProcessorService;
			private IRecursivelyDeleteTtFilesInPathService recursivelyDeleteTtFilesInPathService;

			public TextTemplateZipProcessor(IExtractZipToDirectoryService extractZipToDirectoryService,
									IRecursivelyRenameFilesAndFoldersByConvention recursivelyRenameFilesAndFoldersByConvention,
									ICreateQueueFromPathService createQueueFromPathService,
									IQueueProcessorService queueProcessorService,
									IRecursivelyDeleteTtFilesInPathService recursivelyDeleteTtFilesInPathService)
			{
				this.recursivelyDeleteTtFilesInPathService = recursivelyDeleteTtFilesInPathService;
				this.queueProcessorService = queueProcessorService;
				this.createQueueFromPathService = createQueueFromPathService;
				this.recursivelyRenameFilesAndFoldersByConvention = recursivelyRenameFilesAndFoldersByConvention;
				this.extractZipToDirectoryService = extractZipToDirectoryService;
			}

			public IEnumerable<string> ProcessZip(string pathToZip, string outputPath)
			{
				extractZipToDirectoryService.ExtractToPath(pathToZip, outputPath);

				var queue = createQueueFromPathService.RecursivelyBuildQueueFromPath(outputPath);

				var errors = queueProcessorService.ProcessQueue(queue);

				recursivelyRenameFilesAndFoldersByConvention.RecursivelyRename(outputPath);

				recursivelyDeleteTtFilesInPathService.RecursivelyDeleteTtFilesInPath(outputPath);

				return errors;
			}
		}
	}
}
