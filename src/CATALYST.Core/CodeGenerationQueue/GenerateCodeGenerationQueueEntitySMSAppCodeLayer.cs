using System.Collections.Generic;
using CATALYST.Core.Actions.FileSystem;
using CATALYST.Core.Objects;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;

namespace CATALYST.Core.CodeGenerationQueue
{
	public class GenerateCodeGenerationQueueEntitySMSAppCodeLayer
	{
		private IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction;
		private IConfigurationState configurationState;

		public GenerateCodeGenerationQueueEntitySMSAppCodeLayer(IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction, IConfigurationState configurationState)
		{
			this.removeAllFilesButLeaveDirectoryStructureAction = removeAllFilesButLeaveDirectoryStructureAction;
			this.configurationState = configurationState;
		}

		public IEnumerable<CodeGenerationQueueItem> Execute(T4ProcessState t4ProcessState)
		{
			removeAllFilesButLeaveDirectoryStructureAction.Execute(configurationState.GetBaseOutputPath() + "\\APP_CODE");
			var list = new List<CodeGenerationQueueItem>() 
			{
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\APP_CODE\\MANAGE\\Engine\\ENTITY\\Manage\\ENTITYInputModelClassMap.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\APP_CODE\\MANAGE\\Engine\\{0}\\Manage\\{0}InputModelClassMap.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\APP_CODE\\MANAGE\\Engine\\ENTITY\\ListProcessorStep.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\APP_CODE\\MANAGE\\Engine\\{0}\\ListProcessorStep.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\APP_CODE\\MANAGE\\Engine\\ENTITY\\ManageProcessorStep.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\APP_CODE\\MANAGE\\Engine\\{0}\\ManageProcessorStep.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\APP_CODE\\MANAGE\\Engine\\ENTITYProcessor.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\APP_CODE\\MANAGE\\Engine\\{0}Processor.cs", t4ProcessState.Classname) },
			};
			return list;
		}
	}
}
