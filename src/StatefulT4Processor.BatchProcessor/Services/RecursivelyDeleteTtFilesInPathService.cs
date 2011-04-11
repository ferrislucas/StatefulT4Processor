using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface IRecursivelyDeleteTtFilesInPathService
	{
		void RecursivelyDeleteTtFilesInPath(string path);
	}

	public class RecursivelyDeleteTtFilesInPathService : IRecursivelyDeleteTtFilesInPathService
	{
		public void RecursivelyDeleteTtFilesInPath(string path)
		{
			throw new NotImplementedException();
		}
	}
}
