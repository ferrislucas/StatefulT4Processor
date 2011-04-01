using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.Actions;
using Moq;
using CATALYST.Core.Actions.FileSystem;

namespace CATALYST.Core.Tests.Actions
{
	[TestClass]
	public class RemoveAllFilesButLeaveDirectoryStructureActionTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_FileSystemContains1Dir_VerifyRecursionOnDirectory()
		{
			var removeAllFilesButLeaveDirectoryStructureAction = mocker.Resolve<RemoveAllFilesButLeaveDirectoryStructureAction>();

			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles("folder")).Returns(new string[] { "deleteme" });
			mocker.GetMock<IFileSystem>().Setup(a => a.GetDirectories("path")).Returns(new string[] { "folder" });
			
			removeAllFilesButLeaveDirectoryStructureAction.Execute("path");

			mocker.GetMock<IFileSystem>().Verify(a => a.DeleteFile("deleteme"), Times.Once());
		}

		[TestMethod]
		public void Execute_FileSystemContains1File_VerifyFileSystemDeleteCalledOnFile()
		{
			var removeAllFilesButLeaveDirectoryStructureAction = mocker.Resolve<RemoveAllFilesButLeaveDirectoryStructureAction>();

			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles(It.IsAny<string>())).Returns(new string[] { "deleteme" });

			removeAllFilesButLeaveDirectoryStructureAction.Execute("path");

			mocker.GetMock<IFileSystem>().Verify(a => a.DeleteFile("deleteme"), Times.Once());
		}
	}
}
