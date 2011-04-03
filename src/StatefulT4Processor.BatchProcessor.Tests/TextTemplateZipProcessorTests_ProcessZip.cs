using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.BatchProcessor.Services;
using StatefulT4Processor.BatchProcessor.StatefulT4Processor.BatchProcessor;

namespace StatefulT4Processor.BatchProcessor.Tests
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
		public void Calls_ExtractToPath_method_of_IExtractZipToDirectoryService()
		{
			mocker.Resolve<TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			mocker.GetMock<IExtractZipToDirectoryService>()
				.Verify(a => a.ExtractToPath("pathToZip", "outputPath"), Times.Once());
		}

		[TestMethod]
		public void Calls_RecursivelyProcessAndDeleteT4TemplatesStartingAtPath_method_of_IProcessAndDeleteT4TemplatesService()
		{
			mocker.Resolve<TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			mocker.GetMock<IProcessAndDeleteT4TemplatesService>()
				.Verify(a => a.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors("outputPath"), Times.Once());
		}

		[TestMethod]
		public void Returns_errors_from_IProcessAndDeleteT4TemplatesService()
		{
			mocker.GetMock<IProcessAndDeleteT4TemplatesService>()
				.Setup(a => a.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors(It.IsAny<string>()))
				.Returns(new string[]{"error"});

			var result = mocker.Resolve<TextTemplateZipProcessor>().ProcessZip("pathToZip", "outputPath");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("error", result.First());
		}

		[TestMethod]
		public void Calls_RecursivelyProcessAndDeleteT4TemplatesStartingAtPath_method_after_unpacking_zip()
		{
			var textTemplateZipProcessor = new TextTemplateZipProcessor(new DummyExtractZipToDirectoryService(),
			                                                            mocker.GetMock<IProcessAndDeleteT4TemplatesService>().
			                                                            	Object);
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
	}
}
