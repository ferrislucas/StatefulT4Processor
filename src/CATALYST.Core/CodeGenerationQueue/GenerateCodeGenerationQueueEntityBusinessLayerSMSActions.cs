using System.Collections.Generic;
using CATALYST.Core.Actions.FileSystem;
using CATALYST.Core.Objects;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;

namespace CATALYST.Core.CodeGenerationQueue
{
	public class GenerateCodeGenerationQueueEntityBusinessLayerSMSActions
	{
		private IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction;
		private IConfigurationState configurationState;

		public GenerateCodeGenerationQueueEntityBusinessLayerSMSActions(IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction, IConfigurationState configurationState)
		{
			this.removeAllFilesButLeaveDirectoryStructureAction = removeAllFilesButLeaveDirectoryStructureAction;
			this.configurationState = configurationState;
		}

		public IEnumerable<CodeGenerationQueueItem> Execute(T4ProcessState t4ProcessState)
		{
			removeAllFilesButLeaveDirectoryStructureAction.Execute(configurationState.GetBaseOutputPath() + "\\BUSINESS");
			var list = new List<CodeGenerationQueueItem>() 
			{
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\Actions\\Crud\\CreateAction.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\Actions\\Crud\\CreateAction.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\Actions\\Crud\\UpdateAction.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\Actions\\Crud\\UpdateAction.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\Actions\\Crud\\DeleteAction.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\Actions\\Crud\\DeleteAction.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\Actions\\Crud\\GetAllENTITYsAction.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\Actions\\Crud\\GetAll{0}sAction.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\Actions\\Crud\\GetENTITYByKeyAction.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\Actions\\Crud\\Get{0}ByKeyAction.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\Actions\\Versioning\\GetKeyOfENTITYByVersionTypeAndInstanceKeyAction.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\Actions\\Versioning\\GetKeyOf{0}ByVersionTypeAndInstanceKeyAction.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\InputModels\\Manage\\ENTITYInputModel.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\InputModels\\Manage\\{0}InputModel.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\BUSINESS\\Validators\\Manage\\ENTITYInputModelValidator.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\BUSINESS\\Validators\\Manage\\{0}InputModelValidator.cs", t4ProcessState.Classname) },
			};
			return list;
		}
	}
}
