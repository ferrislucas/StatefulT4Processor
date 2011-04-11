using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.Shared.Models;
using StatefulT4Processor.TextTemplateZipProcessor.Services;

namespace StatefulT4Processor.TextTemplateZipProcessor.Tests
{
	[TestClass]
	public class TextTemplateZipProcessorTests_ProcessZip
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_errors_from_IQueueProcessorService()
		{
			mocker.GetMock<IQueueProcessorService>().Setup(a => a.ProcessQueue(It.IsAny<Queue>()))
				.Returns(new string[]
				         	{
				         		"test",
							});

			var result = mocker.Resolve<StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("test", result.First());
		}

		[TestMethod]
		public void Calls_ProcessQueue_method_of_IQueueProcessorService()
		{
			var queue = new Queue()
			            	{
			            		QueueItems = new QueueItem[]
			            		             	{
			            		             		new QueueItem()
			            		             			{
			            		             				InputPath = "test",
			            		             			}, 
			            						}
			            	};
			mocker.GetMock<ICreateQueueFromPathService>().Setup(a => a.RecursivelyBuildQueueFromPath(It.IsAny<string>()))
				.Returns(queue);

			mocker.Resolve<StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			mocker.GetMock<IQueueProcessorService>().Verify(a => a.ProcessQueue(queue), Times.Once());
		}

		[TestMethod]
		public void Builds_queue_for_output_path()
		{
			mocker.Resolve<StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			mocker.GetMock<ICreateQueueFromPathService>().Verify(a => a.RecursivelyBuildQueueFromPath("outputPath"), Times.Once());
		}

		[TestMethod]
		public void Calls_RecursivelyBuildQueueFromPath_method_after_unpacking_zip()
		{
			var textTemplateZipProcessor = new TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor(new DummyExtractZipToDirectoryService(), mocker.GetMock<IRecursivelyRenameFilesAndFoldersByConvention>().Object, mocker.GetMock<ICreateQueueFromPathService>().Object, mocker.GetMock<IQueueProcessorService>().Object);
			try
			{
				textTemplateZipProcessor.ProcessZip("pathToZip", "outputPath");
			}
			catch (Exception) { }

			mocker.GetMock<ICreateQueueFromPathService>()
				.Verify(a => a.RecursivelyBuildQueueFromPath(It.IsAny<string>()), Times.Never());
		}

		[TestMethod]
		public void Calls_RecursivelyRename_method_of_IRecursivelyRenameFilesAndFoldersByConvention()
		{
			mocker.Resolve<StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			mocker.GetMock<IRecursivelyRenameFilesAndFoldersByConvention>()
				.Verify(a => a.RecursivelyRename("outputPath"), Times.Once());
		}

		[TestMethod]
		public void Calls_RecursivelyRename_method_of_IRecursivelyRenameFilesAndFoldersByConvention_after_calling_IProcessAndDeleteT4TemplatesService()
		{
			var textTemplateZipProcessor =
				new StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor(
					mocker.GetMock<IExtractZipToDirectoryService>().Object, 
					mocker.GetMock<IRecursivelyRenameFilesAndFoldersByConvention>().Object,
					mocker.GetMock<ICreateQueueFromPathService>().Object
					, new DummyQueueProcessorService());
			try
			{
				textTemplateZipProcessor.ProcessZip("pathToZip", "outputPath");
			}catch(Exception){}

			mocker.GetMock<IRecursivelyRenameFilesAndFoldersByConvention>()
				.Verify(a => a.RecursivelyRename(It.IsAny<string>()), Times.Never());
		}

		[TestMethod]
		public void Calls_ExtractToPath_method_of_IExtractZipToDirectoryService()
		{
			mocker.Resolve<TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			mocker.GetMock<IExtractZipToDirectoryService>()
				.Verify(a => a.ExtractToPath("pathToZip", "outputPath"), Times.Once());
		}

		[TestMethod]
		public void Calls_RecursivelyProcessAndDeleteT4TemplatesStartingAtPath_method_after_unpacking_zip()
		{
			var textTemplateZipProcessor = new TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor(new DummyExtractZipToDirectoryService(), mocker.GetMock<IRecursivelyRenameFilesAndFoldersByConvention>().Object, mocker.GetMock<ICreateQueueFromPathService>().Object, mocker.GetMock<IQueueProcessorService>().Object);
			try
			{
				textTemplateZipProcessor.ProcessZip("pathToZip", "outputPath");
			}
			catch (Exception) {}
			
			mocker.GetMock<IProcessAndDeleteT4TemplatesService>()
				.Verify(a => a.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(It.IsAny<string>()), Times.Never());
		}

		private class DummyExtractZipToDirectoryService : IExtractZipToDirectoryService
		{
			public void ExtractToPath(string pathToZip, string pathToExtractTo)
			{
				throw new NotImplementedException();
			}
		}

		public class DummyIProcessAndDeleteT4TemplatesService : IProcessAndDeleteT4TemplatesService
		{
			public IEnumerable<string> RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(string path)
			{
				throw new NotImplementedException();
			}
		}

		public class DummyQueueProcessorService : IQueueProcessorService
		{
			public string[] ProcessQueue(Queue queue)
			{
				throw new NotImplementedException();
			}
		}
	}
}
