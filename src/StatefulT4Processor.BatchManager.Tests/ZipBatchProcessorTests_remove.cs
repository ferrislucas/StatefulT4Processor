using System;
using System.IO;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.Services;

namespace StatefulT4Processor.TextTemplateBatchManager.Tests
{
	[TestClass]
	public class ZipBatchProcessorTests_remove
	{
		private AutoMoqer mocker;
		private Guid guid;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
			guid = Guid.NewGuid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid()).Returns(guid);
			mocker.GetMock<IGetWorkingFolderPath>().Setup(a => a.GetPathToWorkingFolder())
				.Returns("pathToWorkingFolder");
		}

		[TestMethod]
		public void Creates_temp_working_folder()
		{
			mocker.Resolve<ZipBatchProcessor_remove>().ProcessBatch(new TextTemplateBatch());

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.CreateFolder(GetTemporaryWorkingFolderPath()), Times.Once());
		}

		[TestMethod]
		public void Returns_path_to_folder_of_processed_templates()
		{
			var zipBatchId = Guid.NewGuid();
			
			var result = mocker.Resolve<ZipBatchProcessor_remove>().ProcessBatch(new TextTemplateBatch()
																	{
																		Id = zipBatchId.ToString(),
																		ZipFilename = "zipFilename.zip",
																	});

			Assert.AreEqual(GetTemporaryWorkingFolderPath(), result);
		}

		private string GetTemporaryWorkingFolderPath()
		{
			return string.Format("pathToWorkingFolder" + "BatchProcessTemp" + Path.DirectorySeparatorChar + "{0}", guid);
		}
	}
}
