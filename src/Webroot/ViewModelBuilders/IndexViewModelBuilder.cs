using StatefulT4Processor.Webroot.Models;
using StatefulT4Processor.Webroot.Repositories;

namespace StatefulT4Processor.Webroot.ViewModelBuilders
{
	public interface IIndexViewModelBuilder
	{
		IndexViewModel BuildViewModel();
	}

	public class IndexViewModelBuilder : IIndexViewModelBuilder
	{
		private readonly IStateRepository stateRepository;

		public IndexViewModelBuilder(IStateRepository stateRepository)
		{
			this.stateRepository = stateRepository;
		}

		public IndexViewModel BuildViewModel()
		{
			return new IndexViewModel()
			       	{
						States = stateRepository.GetAll(),
			       	};
		}
	}
}