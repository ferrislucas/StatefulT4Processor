using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CATALYST.Core.Actions.FileSystem;
using CATALYST.Core.Objects;
using CATALYST.Core.CodeGenerationQueue;
using CATALYST.Core;
using CATALYST.Core.StateSerialization;
using EasyObjectStore.Helpers;
using StatefulT4Processor.BatchProcessor;
using StatefulT4Processor.BatchProcessor.Helpers;
using StatefulT4Processor.BatchProcessor.Models;
using StatefulT4Processor.BatchProcessor.Services;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;
using FileSystem = CATALYST.Core.FileSystem;
using GuidGetter = StatefulT4Processor.Shared.GuidGetter;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			var zipBatchProcessor = 
				new ZipBatchProcessor(new GuidGetter(), 
									new StatefulT4Processor.Shared.FileSystem(), 
									new GetWorkingFolderPath(), 
									new ExtractZipToDirectoryService(), 
									new ProcessAndDeleteT4TemplatesService(new StatefulT4Processor.Shared.FileSystem(), new T4TemplateHostWrapper()));
			zipBatchProcessor.ProcessBatch(new ZipBatch()
			                               	{
												Id = "batch_id",
												Name = "test",
												ZipFilename = "test.zip",
			                               	});
		}
	}

	public class GetWorkingFolderPath : IGetWorkingFolderPath
	{
		public string GetPathToWorkingFolder()
		{
			return @"C:\Users\lucasf\Desktop\junk\";
		}
	}
}
