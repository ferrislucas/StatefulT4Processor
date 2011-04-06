using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
					new DummyIProcessAndDeleteT4TemplatesService(),
					mocker.GetMock<IRecursivelyRenameFilesAndFoldersByConvention>().Object);
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
		public void Calls_RecursivelyProcessAndDeleteT4TemplatesStartingAtPath_method_of_IProcessAndDeleteT4TemplatesService()
		{
			mocker.Resolve<TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			mocker.GetMock<IProcessAndDeleteT4TemplatesService>()
				.Verify(a => a.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors("outputPath"), Times.Once());
		}

		[TestMethod]
		public void Returns_errors_from_IProcessAndDeleteT4TemplatesService()
		{
			mocker.GetMock<IProcessAndDeleteT4TemplatesService>()
				.Setup(a => a.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(It.IsAny<string>()))
				.Returns(new string[]{"error"});

			var result = mocker.Resolve<TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("error", result.First());
		}

		[TestMethod]
		public void Calls_RecursivelyProcessAndDeleteT4TemplatesStartingAtPath_method_after_unpacking_zip()
		{
			var textTemplateZipProcessor = new TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor.TextTemplateZipProcessor(new DummyExtractZipToDirectoryService(), mocker.GetMock<IProcessAndDeleteT4TemplatesService>().Object, mocker.GetMock<IRecursivelyRenameFilesAndFoldersByConvention>().Object);
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
	}
}
