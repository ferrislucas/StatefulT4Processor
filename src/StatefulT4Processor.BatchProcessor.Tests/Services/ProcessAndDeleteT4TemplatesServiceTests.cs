using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.BatchProcessor.Helpers;
using StatefulT4Processor.BatchProcessor.Services;
using IFileSystem = StatefulT4Processor.Shared.IFileSystem;

namespace StatefulT4Processor.BatchProcessor.Tests.Services
{
	[TestClass]
	public class ProcessAndDeleteT4TemplatesServiceTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.DirectoryExists(It.IsAny<string>()))
				.Returns(false);
		}

		[TestMethod]
		public void Processes_t4_templates_in_path()
		{
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.GetFiles("path"))
				.Returns(new string[]
				         	{
				         		"file1.cs.tt",
								"file2.cs.tt"
							});

			mocker.Resolve<ProcessAndDeleteT4TemplatesService>()
				.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors("path");

			mocker.GetMock<IT4TemplateHostWrapper>()
				.Verify(a => a.ProcessT4File("file1.cs.tt", "file1.cs"), Times.Once());
			mocker.GetMock<IT4TemplateHostWrapper>()
				.Verify(a => a.ProcessT4File("file2.cs.tt", "file2.cs"), Times.Once());
		}

		[TestMethod]
		public void Processes_t4_templates_in_child_directory_of_provided_path()
		{
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.GetDirectories("path"))
				.Returns(new string[]
				         	{
				         		"directory1",
							});
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.GetFiles("directory1"))
				.Returns(new string[]
				         	{
				         		"file1.cs.tt",
								"file2.cs.tt"
							});

			mocker.Resolve<ProcessAndDeleteT4TemplatesService>()
				.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors("path");

			mocker.GetMock<IT4TemplateHostWrapper>()
				.Verify(a => a.ProcessT4File("file1.cs.tt", "file1.cs"), Times.Once());
			mocker.GetMock<IT4TemplateHostWrapper>()
				.Verify(a => a.ProcessT4File("file2.cs.tt", "file2.cs"), Times.Once());
		}

		[TestMethod]
		public void Deletes_t4_templates_in_child_directory_of_provided_path()
		{
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.GetDirectories("path"))
				.Returns(new string[]
				         	{
				         		"directory1",
							});
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.GetFiles("directory1"))
				.Returns(new string[]
				         	{
				         		"file1.cs.tt",
								"file2.cs.tt"
							});

			mocker.Resolve<ProcessAndDeleteT4TemplatesService>()
				.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors("path");

			mocker.GetMock<IFileSystem>()
				.Verify(a => a.DeleteFile("file1.cs.tt"), Times.Once());
			mocker.GetMock<IFileSystem>()
				.Verify(a => a.DeleteFile("file2.cs.tt"), Times.Once());
		}

		[TestMethod]
		public void Calls_Delete_method_of_IFileSystem_after_processing_template()
		{
			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles(It.IsAny<string>()))
				.Returns(new string[] { "file1.tt" });

			try
			{
				GetInstanceOfProcessAndDeleteT4TemplatesServiceWithDummyTemplateProcessor()
					.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors("path");				
			}catch(Exception){}

			mocker.GetMock<IFileSystem>().Verify(a => a.DeleteFile(It.IsAny<string>()), Times.Never());
		}

		[TestMethod]
		public void Returns_Errors_property_of_IT4TemplateHostWrapper()
		{
			mocker.GetMock<IT4TemplateHostWrapper>().Setup(a => a.Errors).Returns(new string[] { "error" });

			mocker.GetMock<IFileSystem>().Setup(a => a.GetFiles(It.IsAny<string>()))
				.Returns(new string[] { "file1.tt" });

			var result = mocker.Resolve<ProcessAndDeleteT4TemplatesService>()
				.RecursivelyProcessAndDeleteT4TemplatesStartingAtPathAndReturnErrors("path");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("error", result.First());
		}

		private ProcessAndDeleteT4TemplatesService GetInstanceOfProcessAndDeleteT4TemplatesServiceWithDummyTemplateProcessor()
		{
			return new ProcessAndDeleteT4TemplatesService(mocker.GetMock<IFileSystem>().Object, new T4TemplateHostWrapper());
		}

		private class T4TemplateHostWrapper : IT4TemplateHostWrapper
		{
			public void ProcessT4File(string inputFile, string outputFile)
			{
				throw new NotImplementedException();
			}

			public IEnumerable<string> Errors
			{
				get { throw new NotImplementedException(); }
			}
		}
	}
}
