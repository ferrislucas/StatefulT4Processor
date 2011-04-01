
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.CodeGenerationQueue;
using Moq;
using System.CodeDom.Compiler;
using CATALYST.Core.Actions.FileSystem;

namespace CATALYST.Core.Tests.CodeGenerationQueue
{
	[TestClass]
	public class CodeGenerationQueueConsumerTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void ExecuteAndReturnErrors_QueueWirhItem_VerifyCreateDirectoryFromFilenameActionCalledForItemOutputFile()
		{
			mocker.GetMock<IT4TemplateHostWrapper>().Setup(a => a.Errors).Returns(new string[] { "error" });

			var codeGenerationQueueConsumer = mocker.Resolve<CodeGenerationQueueConsumer>();
			var result = codeGenerationQueueConsumer.ExecuteAndReturnErrors(new List<CodeGenerationQueueItem>() { new CodeGenerationQueueItem() { OutputFilename = "outputfile" } });

			mocker.GetMock<ICreateDirectoryFromFilenameAction>().Verify(a => a.Execute("outputfile"), Times.Once());
		}

		[TestMethod]
		public void ExecuteAndReturnErrors_T4ProcessorReturns1ErrorPerRun_ReturnsAllErrors()
		{
			mocker.GetMock<IT4TemplateHostWrapper>().Setup(a => a.Errors).Returns(new string[] { "error" });

			var codeGenerationQueueConsumer = mocker.Resolve<CodeGenerationQueueConsumer>();
			var result = codeGenerationQueueConsumer.ExecuteAndReturnErrors(null);

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("error", result.First());
		}

		[TestMethod]
		public void ExecuteAndReturnErrors_QueueWithItems_VerifyTemplateHostIsCAlledAgainstEachItem()
		{
			var queue = new List<CodeGenerationQueueItem>()
			{
				new CodeGenerationQueueItem() { InputFilename = "inputfile1", OutputFilename = "outputfile1" }, 
				new CodeGenerationQueueItem() { InputFilename = "inputfile2", OutputFilename = "outputfile2" }, 
				new CodeGenerationQueueItem() { InputFilename = "inputfile3", OutputFilename = "outputfile3" }, 
			};
			var codeGenerationQueueConsumer = mocker.Resolve<CodeGenerationQueueConsumer>();

			var result = codeGenerationQueueConsumer.ExecuteAndReturnErrors(queue);

			for (var n = 1; n <= 3; n++)
			{
				mocker.GetMock<IT4TemplateHostWrapper>().Verify(a => a.ProcessT4File("inputfile" + n, "outputfile" + n), Times.Once());
			}
		}

		[TestMethod]
		public void ExecuteAndReturnErrors_EmptyQueue_ReturnsEmptyErrors()
		{
			var codeGenerationQueueConsumer = mocker.Resolve<CodeGenerationQueueConsumer>();

			var result = codeGenerationQueueConsumer.ExecuteAndReturnErrors(null);

			Assert.AreEqual(0, result.Count());
		}
	}
}
