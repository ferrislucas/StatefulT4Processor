using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.Shared;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.TextTemplateZipProcessor.Helpers;
using StatefulT4Processor.TextTemplateZipProcessor.Services;

namespace StatefulT4Processor.TextTemplateZipProcessor.Tests.Services
{
	[TestClass]
	public class RecursivelyRenameFilesAndFoldersByConventionTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Calls_Rename_method_of_IFileSystem()
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("test", "test2");
			mocker.GetMock<IT4StateContext>().Setup(a => a.GetDictionaryFromState())
				.Returns(dictionary);
			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles("path"))
				.Returns(new string[]
				         	{
				         		"file1.txt",
								"file2.txt",
							});

			mocker.Resolve<RecursivelyRenameFilesAndFoldersByConvention>()
				.RecursivelyRename("path");

			mocker.GetMock<IRenameFileOrFolderAccordingToConvention>()
				.Verify(a => a.Rename("file1.txt", It.Is<Dictionary<string, string>>(b => b == dictionary)), Times.Once());
			mocker.GetMock<IRenameFileOrFolderAccordingToConvention>()
				.Verify(a => a.Rename("file2.txt", It.Is<Dictionary<string, string>>(b => b == dictionary)), Times.Once());
		}

		[TestMethod]
		public void Recursively_calls_IRenameFileOrFolderAccordingToConvention()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles("directory1"))
				.Returns(new string[]
				         	{
								"file1.txt",
							});
			mocker.GetMock<IFileSystem>().Setup(a => a.GetDirectories("path"))
				.Returns(new string[]
				         	{
				         		"directory1",
							});

			mocker.Resolve<RecursivelyRenameFilesAndFoldersByConvention>()
				.RecursivelyRename("path");

			mocker.GetMock<IRenameFileOrFolderAccordingToConvention>()
				.Verify(a => a.Rename("file1.txt", It.IsAny<Dictionary<string, string>>()), Times.Once());
		}
	}
}
