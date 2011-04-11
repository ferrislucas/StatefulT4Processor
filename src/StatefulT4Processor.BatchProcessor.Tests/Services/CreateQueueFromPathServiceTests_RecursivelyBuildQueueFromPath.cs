using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateZipProcessor.Services;

namespace StatefulT4Processor.TextTemplateZipProcessor.Tests.Services
{
	[TestClass]
	public class CreateQueueFromPathServiceTests_RecursivelyBuildQueueFromPath
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Includes_files_in_first_level_folder()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles("path"))
				.Returns(new string[] {"file1", "file2"});

			var result = mocker.Resolve<CreateQueueFromPathService>().RecursivelyBuildQueueFromPath("path");

			Assert.AreEqual(2, result.QueueItems.Count());
			Assert.AreEqual(1, result.QueueItems.Where(a => a.InputPath == "file1").Count());
			Assert.AreEqual(1, result.QueueItems.Where(a => a.InputPath == "file2").Count());
		}

		[TestMethod]
		public void Includes_files_in_nested_folder()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.GetDirectories("path"))
				.Returns(new string[]
				         	{
				         		"folder1",
							});
			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles("folder1"))
				.Returns(new string[] { "file1", "file2" });

			var result = mocker.Resolve<CreateQueueFromPathService>().RecursivelyBuildQueueFromPath("path");

			Assert.AreEqual(2, result.QueueItems.Count());
			Assert.AreEqual(1, result.QueueItems.Where(a => a.InputPath == "file1").Count());
			Assert.AreEqual(1, result.QueueItems.Where(a => a.InputPath == "file2").Count());
		}
	}
}
