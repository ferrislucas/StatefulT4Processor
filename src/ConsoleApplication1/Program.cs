using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyObjectStore.Helpers;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;
using StatefulT4Processor.TextTemplateZipProcessor.Helpers;
using StatefulT4Processor.TextTemplateZipProcessor.Services;
using StatefulT4Processor.TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor;
using StatefulT4Processor.Webroot.Models;
using FileSystem = StatefulT4Processor.Shared.FileSystem;
using GuidGetter = StatefulT4Processor.Shared.GuidGetter;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			var textTemplateZipProcessor = new TextTemplateZipProcessor(new ExtractZipToDirectoryService(), new ProcessAndDeleteT4TemplatesService(new FileSystem(), new T4TemplateHostWrapper()));
			textTemplateZipProcessor.ProcessZip(@"C:\_Application\StatefulT4Processor\localWorkingFolder\TextTemplateBatchFileUploads\8f37c08d-5769-4e80-86f6-744bc522e81d\test4.zip", @"C:\Users\lucasf\Desktop\junk");
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
