using StatefulT4Processor.Webroot.Models;

namespace StatefulT4Processor.Webroot.ViewModelBuilders
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