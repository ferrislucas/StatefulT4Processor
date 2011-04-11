using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateZipProcessor.Services;

namespace StatefulT4Processor.TextTemplateZipProcessor.Tests.Services
{
	[TestClass]
	public class RecursivelyDeleteTtFilesInPathServiceTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Deletes_text_templates_but_not_other_files()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles("path"))
				.Returns(new string[] {"file1.tt", "file2.txt"});

			mocker.Resolve<RecursivelyDeleteTtFilesInPathService>()
				.RecursivelyDeleteTtFilesInPath("path");

			mocker.GetMock<IFileSystem>().Verify(a => a.DeleteFile("file1.tt"), Times.Once());
			mocker.GetMock<IFileSystem>().Verify(a => a.DeleteFile("file2.txt"), Times.Never());
		}

		[TestMethod]
		public void Deletes_text_templates_but_not_other_files_recursively()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.GetDirectories("path"))
				.Returns(new string[] { "folder1" });
			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles("folder1"))
				.Returns(new string[] { "file1.tt"});

			mocker.Resolve<RecursivelyDeleteTtFilesInPathService>()
				.RecursivelyDeleteTtFilesInPath("path");

			mocker.GetMock<IFileSystem>().Verify(a => a.DeleteFile("file1.tt"), Times.Once());
		}
	}
}
