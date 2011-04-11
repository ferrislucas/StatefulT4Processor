using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.Shared;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.TextTemplateZipProcessor.Helpers;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface IRecursivelyRenameFilesAndFoldersByConvention
	{
		void RecursivelyRename(string path);
	}

	public class RecursivelyRenameFilesAndFoldersByConvention : IRecursivelyRenameFilesAndFoldersByConvention
	{
		private readonly IFileSystem fileSystem;
		private readonly IRenameFileOrFolderAccordingToConvention renameFileOrFolderAccordingToConvention;
		private readonly IT4StateContext t4StateContext;

		public RecursivelyRenameFilesAndFoldersByConvention(IFileSystem fileSystem,
														IRenameFileOrFolderAccordingToConvention renameFileOrFolderAccordingToConvention,
														IT4StateContext t4StateContext)
		{
			this.t4StateContext = t4StateContext;
			this.renameFileOrFolderAccordingToConvention = renameFileOrFolderAccordingToConvention;
			this.fileSystem = fileSystem;
		}

		public void RecursivelyRename(string path)
		{
			foreach(var file in fileSystem.GetFiles(path))
			{
				renameFileOrFolderAccordingToConvention.Rename(file, t4StateContext.GetDictionaryFromState());
			}
			foreach(var directory in fileSystem.GetDirectories(path))
			{
				RecursivelyRename(directory);
				if (directory.Contains(RenameFileOrFolderAccordingToConvention.TokenDelimiter))
					fileSystem.DeleteDirectory(directory);
			}
		}
	}
}
