using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.Actions.FileSystem;
using Moq;

namespace CATALYST.Core.Tests.Actions.FileSystem
{
	[TestClass]
	public class CreateDirectoryFromFilenameActionTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_Filename_VerifyFileSystemCreateDirectoryCalledForParentFolder()
		{
			var createDirectoryIfNotExistsAction = mocker.Resolve<CreateDirectoryFromFilenameAction>();

			createDirectoryIfNotExistsAction.Execute("c:\\path\\test.txt");

			mocker.GetMock<IFileSystem>().Verify(a => a.CreateFolder("c:\\path"), Times.Once());
		}
	}
}
