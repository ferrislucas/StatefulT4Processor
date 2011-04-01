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
	public class GenerateCodeGenerationQueueEntitySMSAppCodeLayerTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		//[TestMethod]
		//public void Execute__ReturnsCLASSInputModelClassMap()
		//{
		//    mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseTemplatePath()).Returns("INPUT");
		//    mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("OUTPUT");
		//    var generateEntityDataLayer = mocker.Resolve<GenerateCodeGenerationQueueEntitySMSAppCodeLayer>();

		//    var result = generateEntityDataLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

		//    Assert.IsTrue(result
		//        .Where(a => a.InputFilename == "INPUT\\APP_CODE\\MANAGE\\Engine\\ENTITY\\Manage\\ENTITYInputModelClassMap.tt")
		//        .Where(a => a.OutputFilename == "OUTPUT\\APP_CODE\\MANAGE\\Engine\\ENTITY\\Manage\\CLASSInputModelClassMap.cs")
		//        .Count() == 1);
		//}

		[TestMethod]
		public void Execute__VerifyRemoveAllFilesButLeaveDirectoryStructureActionCalled()
		{
			mocker.GetMock<IConfigurationState>().Setup(a => a.GetBaseOutputPath()).Returns("path");
			var GenerateEntitySMSAppCodeLayer = mocker.Resolve<GenerateCodeGenerationQueueEntitySMSAppCodeLayer>();

			var result = GenerateEntitySMSAppCodeLayer.Execute(new T4ProcessState() { Classname = "CLASS" });

			mocker.GetMock<IRemoveAllFilesButLeaveDirectoryStructureAction>().Verify(a => a.Execute("path\\APP_CODE"));
		}
	}
}
