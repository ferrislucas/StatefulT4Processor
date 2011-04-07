using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.TextTemplateZipProcessor.Helpers
{
	public interface IRenameFileOrFolderAccordingToConvention
	{
		void Rename(string path, Dictionary<string, string> state);
	}

	public class RenameFileOrFolderAccordingToConvention : IRenameFileOrFolderAccordingToConvention
	{
		public void Rename(string path, Dictionary<string, string> state)
		{
			throw new NotImplementedException();
		}
	}
}
