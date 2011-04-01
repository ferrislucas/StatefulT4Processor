using System.Collections.Generic;
using CATALYST.Core.Actions.FileSystem;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;

namespace CATALYST.Core.CodeGenerationQueue
{
	public class GenerateEntityWebRootSMSLayerAction
	{
		private IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction;
		private IConfigurationState configurationState;

		public GenerateEntityWebRootSMSLayerAction(IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction, IConfigurationState configurationState)
		{
			this.removeAllFilesButLeaveDirectoryStructureAction = removeAllFilesButLeaveDirectoryStructureAction;
			this.configurationState = configurationState;
		}

		public IEnumerable<CodeGenerationQueueItem> Execute(T4ProcessState t4ProcessState)
		{
			removeAllFilesButLeaveDirectoryStructureAction.Execute(configurationState.GetBaseOutputPath() + "\\WEBROOT");
			var list = new List<CodeGenerationQueueItem>() 
			{
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\WEBROOT\\App_\\MANAGE\\Engine\\ENTITY\\ManageViewerStep_ascx.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\WEBROOT\\App_\\MANAGE\\Engine\\{0}\\ManageViewerStep.ascx", t4ProcessState.Classname) },
			};
			return list;
		}
	}
}
