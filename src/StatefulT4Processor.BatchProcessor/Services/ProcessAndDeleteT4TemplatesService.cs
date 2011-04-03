using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.BatchProcessor.Services
{
	public interface IProcessAndDeleteT4TemplatesService
	{
		void RecursivelyProcessAndDeleteT4TemplatesStartingAtPath(string path);
	}

	public class ProcessAndDeleteT4TemplatesService : IProcessAndDeleteT4TemplatesService
	{
		public void RecursivelyProcessAndDeleteT4TemplatesStartingAtPath(string path)
		{
			throw new NotImplementedException();
		}
	}
}
