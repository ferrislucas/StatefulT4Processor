using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CATALYST.Core.CodeGenerationQueue;
using CATALYST.Core.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.Actions;
using CATALYST.Core.Actions.FileSystem;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;

namespace CATALYST.Core.Tests.Tasks
{
	[TestClass]
	public class GenerateCodeGenerationQueueEntityBusinessLayerSMSActionsTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute__ReturnsSMSENTITYInputModel()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\BUSINESS\\InputModels\\Manage\\ENTITYInputModel.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\BUSINESS\\InputModels\\Manage\\CLASSInputModel.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsGetKeyOfENTITYByVersionTypeAndInstanceKeyAction()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\BUSINESS\\Actions\\Versioning\\GetKeyOfENTITYByVersionTypeAndInstanceKeyAction.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\BUSINESS\\Actions\\Versioning\\GetKeyOfCLASSByVersionTypeAndInstanceKeyAction.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsGetENTITYByKeyAction()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\BUSINESS\\Actions\\Crud\\GetENTITYByKeyAction.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\BUSINESS\\Actions\\Crud\\GetCLASSByKeyAction.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsGetAllENTITYsAction()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\BUSINESS\\Actions\\Crud\\GetAllENTITYsAction.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\BUSINESS\\Actions\\Crud\\GetAllCLASSsAction.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsCrudDeleteAction()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\BUSINESS\\Actions\\Crud\\DeleteAction.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\BUSINESS\\Actions\\Crud\\DeleteAction.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsCrudUpdateAction()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\BUSINESS\\Actions\\Crud\\UpdateAction.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\BUSINESS\\Actions\\Crud\\UpdateAction.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__ReturnsCrudCreateAction()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
			var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			Assert.IsTrue(result
				.Where(a => a.InputFilename == "INPUT\\BUSINESS\\Actions\\Crud\\CreateAction.tt")
				.Where(a => a.OutputFilename == "OUTPUT\\BUSINESS\\Actions\\Crud\\CreateAction.cs")
				.Count() == 1);
		}

		[TestMethod]
		public void Execute__VerifyRemoveAllFilesButLeaveDirectoryStructureActionCalled()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("path");
			var generateEntityBusinessLayerSMSActions = mocker.Resolve<GenerateCodeGenerationQueueEntityBusinessLayerSMSActions>();

			var result = generateEntityBusinessLayerSMSActions.Execute(new T4ProcessState() { Classname = "CLASS" });

			mocker.GetMock<IRemoveAllFilesButLeaveDirectoryStructureAction>().Verify(a => a.Execute("path\\BUSINESS"));
		}
	}
}
