using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.BatchProcessor.Helpers;
using StatefulT4Processor.BatchProcessor.Models;
using StatefulT4Processor.BatchProcessor.Services;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.BatchProcessor.Tests
{
	[TestClass]
	public class ZipBatchProcessorTests
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
			mocker.Resolve<ZipBatchProcessor>().ProcessBatch(new ZipBatch());

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.CreateFolder(GetTemporaryWorkingFolderPath()), Times.Once());
		}

		[TestMethod]
		public void Unzips_file_to_temp_working_folder()
		{
			var zipBatchId = Guid.NewGuid();
			var path = string.Format("pathToWorkingFolder" + 
										"BatchProcessFileUploads" +
										Path.DirectorySeparatorChar + 
										zipBatchId + 
										Path.DirectorySeparatorChar + 
										"zipFilename.zip");

			mocker.Resolve<ZipBatchProcessor>().ProcessBatch(new ZipBatch()
			                                                 	{
																	Id = zipBatchId.ToString(),
																	ZipFilename = "zipFilename.zip",
			                                                 	});

			mocker.GetMock<IExtractZipToDirectoryService>()
				.Verify(a => a.ExtractToPath(path, GetTemporaryWorkingFolderPath()), Times.Once());
		}

		[TestMethod]
		public void Calls_RecursivelyProcessAndDeleteT4TemplatesStartingAtPath_method_of_IProcessAndDeleteT4TemplatesService()
		{
			var zipBatchId = Guid.NewGuid();
			mocker.Resolve<ZipBatchProcessor>().ProcessBatch(new ZipBatch()
																{
																	Id = zipBatchId.ToString(),
																	ZipFilename = "zipFilename.zip",
																});

			mocker.GetMock<IProcessAndDeleteT4TemplatesService>()
				.Verify(a => a.RecursivelyProcessAndDeleteT4TemplatesStartingAtPath(GetTemporaryWorkingFolderPath()), Times.Once());
		}

		[TestMethod]
		public void Returns_path_to_folder_of_processed_templates()
		{
			var zipBatchId = Guid.NewGuid();
			
			var result = mocker.Resolve<ZipBatchProcessor>().ProcessBatch(new ZipBatch()
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
