using System.Collections.Generic;
using CATALYST.Core.Actions.FileSystem;
using CATALYST.Core.Objects;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;

namespace CATALYST.Core.CodeGenerationQueue
{
	public class GenerateCodeGenerationQueueEntityDataLayer
	{
		private readonly IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction;
		private readonly IConfigurationState configurationState;

		public GenerateCodeGenerationQueueEntityDataLayer(IRemoveAllFilesButLeaveDirectoryStructureAction removeAllFilesButLeaveDirectoryStructureAction, IConfigurationState configurationState)
		{
			this.removeAllFilesButLeaveDirectoryStructureAction = removeAllFilesButLeaveDirectoryStructureAction;
			this.configurationState = configurationState;
		}

		public IEnumerable<CodeGenerationQueueItem> Execute(T4ProcessState t4ProcessState)
		{
			removeAllFilesButLeaveDirectoryStructureAction.Execute(configurationState.GetBaseOutputPath() + "\\DATA");
			var list = new List<CodeGenerationQueueItem>() 
			{
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\DATA\\ObjectContext\\ENTITYObjectContext.tt", OutputFilename = configurationState.GetBaseOutputPath() +  string.Format("\\DATA\\ObjectContext\\{0}ObjectContext.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\DATA\\Objects\\PartialEntityClass.tt", OutputFilename = configurationState.GetBaseOutputPath() + string.Format("\\DATA\\Objects\\{0}.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\DATA\\Repositories\\AddRepository.tt", OutputFilename = configurationState.GetBaseOutputPath() + string.Format("\\DATA\\Repositories\\{0}AddRepository.cs", t4ProcessState.Classname) }, 
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\DATA\\Repositories\\UpdateRepository.tt", OutputFilename = configurationState.GetBaseOutputPath() + string.Format("\\DATA\\Repositories\\{0}UpdateRepository.cs", t4ProcessState.Classname) },
				new CodeGenerationQueueItem() { InputFilename = configurationState.GetBaseTemplatePath() + "\\DATA\\Repositories\\QueryRepository.tt", OutputFilename = configurationState.GetBaseOutputPath() + string.Format("\\DATA\\Repositories\\{0}QueryRepository.cs", t4ProcessState.Classname) },
			};
			return list;
		}
	}
}
