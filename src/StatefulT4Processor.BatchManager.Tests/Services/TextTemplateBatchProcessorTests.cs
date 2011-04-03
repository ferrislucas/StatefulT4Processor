using System;
using System.IO;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.Services;
using StatefulT4Processor.TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor;

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
			mocker.Resolve<TextTemplateBatchProcessor>().ProcessBatch(new TextTemplateBatch());

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.CreateFolder(GetTemporaryWorkingFolderPath()), Times.Once());
		}

		[TestMethod]
		public void Returns_path_to_folder_of_processed_templates()
		{
			var zipBatchId = Guid.NewGuid();
			
			var result = mocker.Resolve<TextTemplateBatchProcessor>().ProcessBatch(new TextTemplateBatch()
																	{
																		Id = zipBatchId.ToString(),
																		ZipFilename = "zipFilename.zip",
																	});

			Assert.AreEqual(GetTemporaryWorkingFolderPath(), result);
		}

		[TestMethod]
		public void Calls_ProcessZip_method_of_ITextTemplateZipProcessor()
		{
			mocker.GetMock<IGetWorkingFolderPath>().Setup(a => a.GetPathToWorkingFolder()).Returns("workingFolderPath");

			mocker.Resolve<TextTemplateBatchProcessor>().ProcessBatch(new TextTemplateBatch()
			                                                        	{
			                                                        		Id = "id",
																			ZipFilename = "filename.zip"
			                                                        	});

			mocker.GetMock<ITextTemplateZipProcessor>()
				.Verify(a => a.ProcessZip("workingFolderPath" + TextTemplateBatchManagerSettings.TextTemplateBatchFileUploadFolderName + Path.DirectorySeparatorChar + "id" + Path.DirectorySeparatorChar + "filename.zip", string.Format("workingFolderPath" + TextTemplateBatchManagerSettings.TextTemplateBatchProcessTemporaryFolderName + Path.DirectorySeparatorChar + "{0}", guid)), Times.Once());
		}

		private string GetTemporaryWorkingFolderPath()
		{
			return string.Format("pathToWorkingFolder" + TextTemplateBatchManagerSettings.TextTemplateBatchProcessTemporaryFolderName + Path.DirectorySeparatorChar + "{0}", guid);
		}
	}
}
