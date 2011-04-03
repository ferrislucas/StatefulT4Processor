using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace StatefulT4Processor.BatchProcessor.Services
{
	public interface IExtractZipToDirectoryService
	{
		void ExtractToPath(string pathToZip, string pathToExtractTo);
	}

	public class ExtractZipToDirectoryService : IExtractZipToDirectoryService
	{
		public void ExtractToPath(string pathToZip, string pathToExtractTo)
		{
			Extract(pathToZip, pathToExtractTo);
		}

		private static void Extract(string path, string pathToExtractTo)
		{
			using (ZipInputStream s = new ZipInputStream(File.OpenRead(path)))
			{

				ZipEntry theEntry;
				while ((theEntry = s.GetNextEntry()) != null)
				{

					//Console.WriteLine(theEntry.Name);

					string directoryName = Path.GetDirectoryName(pathToExtractTo + Path.DirectorySeparatorChar + theEntry.Name);
					string fileName = directoryName + Path.DirectorySeparatorChar + Path.GetFileName(theEntry.Name);

					// create directory
					if (directoryName.Length > 0)
					{
						Directory.CreateDirectory(directoryName);
					}

					if (fileName != String.Empty)
					{
						using (FileStream streamWriter = File.Create(fileName))
						{

							int size = 2048;
							byte[] data = new byte[2048];
							while (true)
							{
								size = s.Read(data, 0, data.Length);
								if (size > 0)
								{
									streamWriter.Write(data, 0, size);
								}
								else
								{
									break;
								}
							}
						}
					}
				}
			}
		}
	}
}
