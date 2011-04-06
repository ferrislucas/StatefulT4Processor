using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface IRecursivelyRenameFilesAndDirectoriesByConventionService
	{
		void RecursivelyRename(string path);
	}

	public class RecursivelyRenameFilesAndDirectoriesByConventionService : IRecursivelyRenameFilesAndDirectoriesByConventionService
	{
		public void RecursivelyRename(string path)
		{
			throw new NotImplementedException();
		}
	}
}
