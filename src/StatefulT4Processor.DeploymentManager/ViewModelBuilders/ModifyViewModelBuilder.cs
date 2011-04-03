using StatefulT4Processor.DeploymentManager.Models;

namespace StatefulT4Processor.DeploymentManager.ViewModelBuilders
{
	public interface IModifyViewModelBuilder
	{
		ModifyViewModel BuildViewModel(InputModel userInputModel);
	}

	public class ModifyViewModelBuilder : IModifyViewModelBuilder
	{
		public ModifyViewModel BuildViewModel(InputModel userInputModel)
		{
			return new ModifyViewModel()
			       	{
			       		InputModel = userInputModel ?? new InputModel()
			       	};
		}
	}
}