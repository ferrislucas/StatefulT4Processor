using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CATALYST.Core.Actions.FileSystem
{
	public interface ICreateDirectoryFromFilenameAction
	{
		void Execute(string path);
	}

	public class CreateDirectoryFromFilenameAction : ICreateDirectoryFromFilenameAction
	{
		private IFileSystem fileSystem;

		public CreateDirectoryFromFilenameAction(IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public void Execute(string path)
		{
			try
			{
				var createPath = Directory.GetParent(path);
				fileSystem.CreateFolder(createPath.FullName);
			}
			catch (Exception) { }
		}
	}
}
