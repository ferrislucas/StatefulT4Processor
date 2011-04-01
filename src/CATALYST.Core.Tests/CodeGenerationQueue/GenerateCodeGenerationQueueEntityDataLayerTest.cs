using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CATALYST.Core.CodeGenerationQueue;
using CATALYST.Core.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.Actions;
using Moq;
using CATALYST.Core.Actions.FileSystem;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;

namespace CATALYST.Core.Tests.Tasks
{
	[TestClass]
	public class GenerateCodeGenerationQueueEntityDataLayerTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute__ReturnsQueryRepositoryCodeGenerationQueueItem()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityDataLayer>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\DATA\\Repositories\\QueryRepository.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\DATA\\Repositories\\CLASSQueryRepository.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsUpdateRepositoryCodeGenerationQueueItem()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityDataLayer>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\DATA\\Repositories\\UpdateRepository.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\DATA\\Repositories\\CLASSUpdateRepository.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsAddRepositoryCodeGenerationQueueItem()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityDataLayer>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });
			
			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\DATA\\Repositories\\AddRepository.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\DATA\\Repositories\\CLASSAddRepository.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsPartialEntityClassCodeGenerationQueueItem()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityDataLayer>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\DATA\\Objects\\PartialEntityClass.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\DATA\\Objects\\CLASS.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsENTITYObjectContextCodeGenerationQueueItem()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityDataLayer>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" } );

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\DATA\\ObjectContext\\ENTITYObjectContext.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\DATA\\ObjectContext\\CLASSObjectContext.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__VerifyRemoveAllFilesButLeaveDirectoryStructureActionCalled()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("path");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityDataLayer>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			mocker.GetMock<IRemoveAllFilesButLeaveDirectoryStructureAction>().Verify(a => a.Execute("path\\DATA"));
		}
	}
}
