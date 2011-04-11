using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface IRecursivelyDeleteTtFilesInPathService
	{
		void RecursivelyDeleteTtFilesInPath(string path);
	}

	public class RecursivelyDeleteTtFilesInPathService : IRecursivelyDeleteTtFilesInPathService
	{
		private IFileSystem fileSystem;

		public RecursivelyDeleteTtFilesInPathService(IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public void RecursivelyDeleteTtFilesInPath(string path)
		{
			foreach (var file in fileSystem.GetFiles(path))
			{
				if (file.EndsWith(".tt"))
					fileSystem.DeleteFile(file);
			}
			foreach (var directory in fileSystem.GetDirectories(path))
			{
				RecursivelyDeleteTtFilesInPath(directory);
			}
		}
	}
}
