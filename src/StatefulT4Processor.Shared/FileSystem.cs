using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.Shared
{
	public interface IFileSystem
	{
		Stream OpenWrite(string filePath);
		Stream OpenRead(string filePath);
		void Copy(string sourceFilePath, string destinationFilePath);
		void DeleteFile(string filePath);
		Stream Create(string filePath);
		void CreateFolder(string path);
		string CreateTempFile(string directoryPath);
		void CopyFolder(string sourcePath, string targetPath);
		string[] GetFiles(string path);
		string[] GetFiles(string path, string searchPattern);
		string GetFileContents(string path);
		void Move(string sourcePath, string destinationPath);
		FileInformation GetFileInformation(string path);
		bool FileExists(string path);
		bool DirectoryExists(string path);
		void DeleteDirectory(string path);
		string[] GetDirectories(string path);
	}

	public class FileInformation
	{
		public System.DateTime CreationTime { get; set; }
		public System.DateTime LastAccessTime { get; set; }
	}

	public class FileSystem : IFileSystem
	{
		public void DeleteDirectory(string path)
		{
			foreach (var file in GetFiles(path))
			{
				File.Delete(file);
			}
			foreach (var directory in GetDirectories(path))
			{
				DeleteDirectory(directory);
			}
			Directory.Delete(path);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public string[] GetDirectories(string path)
		{
			return Directory.GetDirectories(path);
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
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
			var sourceDirectory = new DirectoryInfo(sourcePath);
			if (sourceDirectory.Exists == false)
			{
				return;
			}

			var targetDirectory = new DirectoryInfo(targetPath);
			if (targetDirectory.Exists == false)
				targetDirectory.Create();

			foreach (var file in sourceDirectory.GetFiles())
				file.CopyTo(Path.Combine(targetPath, file.Name), true);

			foreach (var directory in sourceDirectory.GetDirectories())
				CopyFolder(Path.Combine(sourcePath, directory.Name), Path.Combine(targetPath, directory.Name));
		}

		public string[] GetFiles(string path)
		{
			return Directory.GetFiles(path);
		}

		public string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(path, searchPattern);
		}

		public string GetFileContents(string path)
		{
			if (!File.Exists(path)) return null;
			var myFile = new StreamReader(path);
			var fileContents = myFile.ReadToEnd();
			myFile.Close();
			return fileContents;
		}

		public void Move(string sourcePath, string destinationPath)
		{
			File.Move(sourcePath, destinationPath);
		}

		public FileInformation GetFileInformation(string path)
		{
			var fileInfo = new FileInfo(path);
			if (fileInfo.Exists)
			{
				return new FileInformation()
				{
					CreationTime = fileInfo.CreationTime,
					LastAccessTime = fileInfo.LastAccessTime,
				};
			}
			return null;
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

		public void CreateFolder(string path)
		{
			Directory.CreateDirectory(path);
		}

		public string CreateTempFile(string directoryPath)
		{
			var tempFile = Path.Combine(directoryPath, Path.GetRandomFileName());
			File.Create(tempFile).Dispose();
			return tempFile;
		}
	}
}
