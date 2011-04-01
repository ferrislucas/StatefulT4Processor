using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CATALYST.Core.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.StateSerialization;
using CATALYST.Core.CodeGenerationQueue;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;

namespace CATALYST.Core.Tests.CodeGenerationQueue
{
	[TestClass]
	public class CreateCodeGenerationQueueFromXmlFileTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_ValidFilCcontents_ReturnsCorrectOutputFilename()
		{
			mocker.GetMock<ICore>().Setup(a => a.GetState()).Returns(new T4ProcessState() { Classname = "className" });

			var createCodeGenerationQueueFromXmlFile = mocker.Resolve<CreateCodeGenerationQueueFromXml>();
			var result = createCodeGenerationQueueFromXmlFile.Execute(@"<Root><T4File InputFilename=""TestTemplate.tt"" OutputFilename=""OutputFilename_::Classname::.cs"" /></Root>");

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(@"OutputFilename_className.cs", result.First().OutputFilename);
		}

		[TestMethod]
		public void Execute_ValidFilCcontents_ReturnsCorrectInputFilename()
		{
			mocker.GetMock<ICore>().Setup(a => a.GetState()).Returns(new T4ProcessState(){ Classname = "className"});
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute("filepath")).Returns(xml);

			var createCodeGenerationQueueFromXmlFile = mocker.Resolve<CreateCodeGenerationQueueFromXml>();
			var result = createCodeGenerationQueueFromXmlFile.Execute(xml);

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(@"InputFilename.tt", result.First().InputFilename);
		}


		[TestMethod]
		public void Execute_EmptyFilCcontents_ReturnsEmptyQueue()
		{
			var createCodeGenerationQueueFromXmlFile = mocker.Resolve<CreateCodeGenerationQueueFromXml>();
			var result = createCodeGenerationQueueFromXmlFile.Execute(null);

			Assert.AreEqual(0, result.Count());
		}

		public const string xml = @"<?xml version=""1.0"" ?>
				<Root>
					<T4File InputFilename=""InputFilename.tt"" OutputFilename=""OutputFilename_::Classname::.cs"" />
				</Root>";
	}
}
