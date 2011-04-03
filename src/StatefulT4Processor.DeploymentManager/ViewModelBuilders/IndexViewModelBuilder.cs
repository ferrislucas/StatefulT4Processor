using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Repositories;

namespace StatefulT4Processor.DeploymentManager.ViewModelBuilders
{
	public interface IIndexViewModelBuilder
	{
		IndexViewModel BuildViewModel();
	}

	public class IndexViewModelBuilder : IIndexViewModelBuilder
	{
		private readonly IWidgetRepository widgetRepository;

		public IndexViewModelBuilder(IWidgetRepository widgetRepository)
		{
			this.widgetRepository = widgetRepository;
		}

		public IndexViewModel BuildViewModel()
		{
			return new IndexViewModel()
			       	{
						Deployments = widgetRepository.GetAll(),
			       	};
		}
	}
}