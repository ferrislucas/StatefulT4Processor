using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.Repositories;
using StatefulT4Processor.TextTemplateBatchManager.Shared;

namespace StatefulT4Processor.TextTemplateBatchManager.Contexts
{
	public class TextTemplateBatchContext : ITextTemplateBatchContext
	{
		private readonly ITextTemplateBatchRepository textTemplateBatchRepository;

		public TextTemplateBatchContext(ITextTemplateBatchRepository textTemplateBatchRepository)
		{
			this.textTemplateBatchRepository = textTemplateBatchRepository;
		}

		public IEnumerable<TextTemplateBatch> GetAll()
		{
			return textTemplateBatchRepository.GetAll();
		}
	}
}