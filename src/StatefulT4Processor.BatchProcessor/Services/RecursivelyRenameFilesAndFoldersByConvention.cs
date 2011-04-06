using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface IRecursivelyRenameFilesAndFoldersByConvention
	{
		void RecursivelyRename(string path);
	}

	public class RecursivelyRenameFilesAndFoldersByConvention : IRecursivelyRenameFilesAndFoldersByConvention
	{
		public void RecursivelyRename(string path)
		{
			throw new NotImplementedException();
		}
	}
}
