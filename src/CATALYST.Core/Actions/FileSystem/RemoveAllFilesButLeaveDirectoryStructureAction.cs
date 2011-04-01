using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CATALYST.Core.Actions.FileSystem
{
	public interface IRemoveAllFilesButLeaveDirectoryStructureAction
	{
		void Execute(string path);
	}

	public class RemoveAllFilesButLeaveDirectoryStructureAction : IRemoveAllFilesButLeaveDirectoryStructureAction
	{
		private IFileSystem fileSystem;

		public RemoveAllFilesButLeaveDirectoryStructureAction(IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public void Execute(string path)
		{
			foreach (var file in fileSystem.GetFiles(path))
			{
				fileSystem.DeleteFile(file);
			}
			foreach (var directory in fileSystem.GetDirectories(path))
			{
				Execute(directory);
			}
		}
	}
}
