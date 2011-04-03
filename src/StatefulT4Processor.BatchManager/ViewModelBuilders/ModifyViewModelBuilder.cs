using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatefulT4Processor.TextTemplateBatchManager.Models;

namespace StatefulT4Processor.TextTemplateBatchManager.ViewModelBuilders
{
	public interface IModifyViewModelBuilder
	{
		ModifyViewModel BuildViewModel(TextTemplateModifyInputModel modifyInputModel);
	}

	public class ModifyViewModelBuilder : IModifyViewModelBuilder
	{
		public ModifyViewModel BuildViewModel(TextTemplateModifyInputModel modifyInputModel)
		{
			return new ModifyViewModel()
			       	{
			       		ModifyInputModel = modifyInputModel ?? new TextTemplateModifyInputModel(),
			       	};
		}
	}
}