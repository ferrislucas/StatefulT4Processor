using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.BatchProcessor.Services;

namespace ConsoleApplication2
{
	class Program_ziptest
	{
		static void Main(string[] args)
		{
			var extractZipToDirectoryService = new ExtractZipToDirectoryService();
			extractZipToDirectoryService.ExtractToPath(@"C:\Users\lucasf\AppData\Local\Temp\SharpZipLib_0860_SourceSamples.zip", @"C:\Users\lucasf\Desktop\junk\");
		}
	}
}
