using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CATALYST.Core.StateSerialization
{
	public interface IGetFileContents
	{
		string Execute(string path);
	}

	public class GetFileContents : IGetFileContents
	{
		public string Execute(string path)
		{
			var content = string.Empty;
			TextReader tr = new StreamReader(path);
			try
			{
				content = tr.ReadToEnd();	
			}
			catch (Exception) {}
			tr.Close();
			return content;
		}
	}
}
