using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.BatchProcessor.Helpers;
using IFileSystem = StatefulT4Processor.Shared.IFileSystem;

namespace StatefulT4Processor.BatchProcessor.Services
{
	public interface IProcessAndDeleteT4TemplatesService
	{
		IEnumerable<string> RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(string path);
	}

	public class ProcessAndDeleteT4TemplatesService : IProcessAndDeleteT4TemplatesService
	{
		private readonly IFileSystem fileSystem;
		private readonly IT4TemplateHostWrapper t4TemplateHostWrapper;

		public ProcessAndDeleteT4TemplatesService(IFileSystem fileSystem, IT4TemplateHostWrapper t4TemplateHostWrapper)
		{
			this.t4TemplateHostWrapper = t4TemplateHostWrapper;
			this.fileSystem = fileSystem;
		}

		public IEnumerable<string> RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(string path)
		{
			foreach(var file in fileSystem.GetFiles(path))
			{
				t4TemplateHostWrapper.ProcessT4File(file, file.Replace(".tt", string.Empty));
				fileSystem.DeleteFile(file);
			}

			foreach (var directory in fileSystem.GetDirectories(path))
			{
				RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(directory);
			}

			return t4TemplateHostWrapper.Errors;
		}
	}
}
