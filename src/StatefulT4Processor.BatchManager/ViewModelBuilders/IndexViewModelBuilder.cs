using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.Repositories;

namespace StatefulT4Processor.TextTemplateBatchManager.ViewModelBuilders
{
	public interface IIndexViewModelBuilder
	{
		IndexViewModel BuildViewModel();
	}

	public class IndexViewModelBuilder : IIndexViewModelBuilder
	{
		private readonly ITextTemplateBatchRepository textTemplateBatchRepository;

		public IndexViewModelBuilder(ITextTemplateBatchRepository textTemplateBatchRepository)
		{
			this.textTemplateBatchRepository = textTemplateBatchRepository;
		}

		public IndexViewModel BuildViewModel()
		{
			return new IndexViewModel()
			       	{
			       		TextTemplateBatches = textTemplateBatchRepository.GetAll().OrderBy(a => a.Name)
			       	};
		}
	}
}