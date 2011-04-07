using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateZipProcessor.Helpers;

namespace StatefulT4Processor.TextTemplateZipProcessor.Tests.Helpers
{
	[TestClass]
	public class RenameFileOrFolderAccordingToConventionTests
	{
		private AutoMoqer mocker;
		private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
			dictionary.Add("TOKEN", "VALUE");
		}

		[TestMethod]
		public void Calls_Move_method_of_IFileSystem_with_appropriate_arguments_when_a_filename_is_encountered_with_a_token_in_it()
		{
			mocker.Resolve<RenameFileOrFolderAccordingToConvention>().Rename("path/test_-_TOKEN_-_.txt", dictionary);

			mocker.GetMock<IFileSystem>().Verify(a => a.Move("path/test" + RenameFileOrFolderAccordingToConvention.TokenDelimiter +"TOKEN" + RenameFileOrFolderAccordingToConvention.TokenDelimiter + ".txt", "path/testVALUE.txt"), Times.Once());
		}

		[TestMethod]
		public void Does_not_call_Move_method_of_IFileSystem_when_there_are_no_tokens_in_the_path()
		{
			mocker.Resolve<RenameFileOrFolderAccordingToConvention>().Rename("path/test.txt", dictionary);

			mocker.GetMock<IFileSystem>().Verify(a => a.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
		}

		[TestMethod]
		public void Creates_folder_if_it_does_not_already_exist()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.DirectoryExists("path/testVALUE")).Returns(false);
			mocker.GetMock<IFileSystem>().Setup(a => a.IsDirectory("path/test_-_TOKEN_-_")).Returns(true);

			mocker.Resolve<RenameFileOrFolderAccordingToConvention>().Rename("path/test_-_TOKEN_-_", dictionary);

			mocker.GetMock<IFileSystem>().Verify(a => a.CreateFolder("path/testVALUE"), Times.Once());
		}

		[TestMethod]
		public void Does_not_create_folder_if_it_already_exists()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.DirectoryExists("path/testVALUE")).Returns(true);

			mocker.Resolve<RenameFileOrFolderAccordingToConvention>().Rename("path/test_-_TOKEN_-_", dictionary);

			mocker.GetMock<IFileSystem>().Verify(a => a.CreateFolder("path/testVALUE"), Times.Never());
		}

		[TestMethod]
		public void Does_not_create_folder_if_the_source_item_being_moved_is_not_a_folder()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.DirectoryExists("path/testVALUE")).Returns(false);
			mocker.GetMock<IFileSystem>().Setup(a => a.IsDirectory("path/test_-_TOKEN_-_")).Returns(false);

			mocker.Resolve<RenameFileOrFolderAccordingToConvention>().Rename("path/test_-_TOKEN_-_", dictionary);

			mocker.GetMock<IFileSystem>().Verify(a => a.CreateFolder("path/testVALUE"), Times.Never());
		}

	}
}
