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

	}
}
