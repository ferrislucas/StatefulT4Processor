using System;
using System.IO;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace CATALYST.Core
{
	public interface IFileSystem
	{
		Stream OpenWrite(string filePath);
		Stream OpenRead(string filePath);
		void Copy(string sourceFilePath, string destinationFilePath);
		void DeleteFile(string filePath);
		void DeleteFolder(string path);
		void CreateFolder(string path);
		Stream Create(string filePath);
		string CreateTempFile(string directoryPath);
		void CopyFolder(string sourcePath, string targetPath);
		IEnumerable<string> GetFiles(string path);
		IEnumerable<string> GetDirectories(string path);
	}

	public class FileSystem : IFileSystem
	{
		public IEnumerable<string> GetFiles(string path)
		{
			var list = new List<string>();
			try
			{
				list.AddRange(Directory.GetFiles(path));
			}
			catch (Exception)
			{
				return list;
			}
			return list;
		}

		public void CreateFolder(string path)
		{
			Directory.CreateDirectory(path);
		}

		public IEnumerable<string> GetDirectories(string path)
		{
			var list = new List<string>();
			try
			{
				list.AddRange(Directory.GetDirectories(path));
			}
			catch (Exception){}
			return list;
		}

		public Stream OpenWrite(string filePath)
		{
			return File.OpenWrite(filePath);
		}

		public Stream OpenRead(string filePath)
		{
			return File.OpenRead(filePath);
		}

		public void CopyFolder(string sourcePath, string targetPath)
		{
//HttpContext.Current.Response.Write("TEST: (FileSystem.CopyFolder) from: " + sourcePath + " :: to: " + targetPath+ " <br />");
			var sourceDirectory = new DirectoryInfo(sourcePath);
			if (sourceDirectory.Exists == false)
			{
//HttpContext.Current.Response.Write("TEST: SOURCE FOLDER DOES NOT EXIST <br />");
				return;
			}

			var targetDirectory = new DirectoryInfo(targetPath);
			if (targetDirectory.Exists == false)
				targetDirectory.Create();

			foreach (var file in sourceDirectory.GetFiles())
				file.CopyTo(Path.Combine(targetPath, file.Name), true);
//HttpContext.Current.Response.Write("TEST: (FileSystem.CopyFolder) DONE: " + sourcePath + " :: to: " + targetPath + " <br />");
		}

		public void DeleteFolder(string path)
		{
			Directory.Delete(path);
		}

		public void Copy(string sourceFilePath, string destinationFilePath)
		{
			File.Copy(sourceFilePath, destinationFilePath, true);
		}

		public void DeleteFile(string filePath)
		{
			File.Delete(filePath);
		}

		public Stream Create(string filePath)
		{
			return File.Create(filePath);
		}

		public string CreateTempFile(string directoryPath)
		{
			var tempFile = Path.Combine(directoryPath, Path.GetRandomFileName());
			File.Create(tempFile).Dispose();
			return tempFile;
		}
	}
}