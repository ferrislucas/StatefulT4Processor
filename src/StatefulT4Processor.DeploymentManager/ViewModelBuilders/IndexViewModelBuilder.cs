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
		private readonly IDeploymentRepository deploymentRepository;

		public IndexViewModelBuilder(IDeploymentRepository deploymentRepository)
		{
			this.deploymentRepository = deploymentRepository;
		}

		public IndexViewModel BuildViewModel()
		{
			return new IndexViewModel()
			       	{
						Deployments = deploymentRepository.GetAll(),
			       	};
		}
	}
}