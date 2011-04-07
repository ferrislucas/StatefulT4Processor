using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.TextTemplateZipProcessor.Helpers
{
	public interface IRenameFileOrFolderAccordingToConvention
	{
		void Rename(string path, Dictionary<string, string> state);
	}

	public class RenameFileOrFolderAccordingToConvention : IRenameFileOrFolderAccordingToConvention
	{
		public const string TokenDelimiter = "_-_";
		private readonly IFileSystem fileSystem;

		public RenameFileOrFolderAccordingToConvention(IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public void Rename(string path, Dictionary<string, string> state)
		{
			var newPath = path;
			foreach (var item in state)
			{
				newPath = newPath.Replace(TokenDelimiter + item.Key + TokenDelimiter, item.Value);
			}

			if (path != newPath)
			{
				if ((!fileSystem.DirectoryExists(newPath)) && (fileSystem.IsDirectory(path)))
					fileSystem.CreateFolder(newPath);

				fileSystem.Move(path, newPath);
			}
				
		}
	}
}
