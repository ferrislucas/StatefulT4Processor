using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatefulT4Processor.TextTemplateBatchManager.Mappers;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.Repositories;

namespace StatefulT4Processor.TextTemplateBatchManager.Services
{
	public interface IProcessTextTemplateBatchModifyInputModelService
	{
		string ProcessAndReturnId(TextTemplateModifyInputModel modifyInputModel);
	}

	public class ProcessTextTemplateBatchModifyInputModelService : IProcessTextTemplateBatchModifyInputModelService
	{
		private readonly ITextTemplateBatchModifyInputModelToTextTemplateBatchMapper textTemplateBatchModifyInputModelToTextTemplateBatchMapper;
		private readonly ITextTemplateBatchRepository textTemplateBatchRepository;

		public ProcessTextTemplateBatchModifyInputModelService(ITextTemplateBatchModifyInputModelToTextTemplateBatchMapper textTemplateBatchModifyInputModelToTextTemplateBatchMapper,
																ITextTemplateBatchRepository textTemplateBatchRepository)
		{
			this.textTemplateBatchRepository = textTemplateBatchRepository;
			this.textTemplateBatchModifyInputModelToTextTemplateBatchMapper = textTemplateBatchModifyInputModelToTextTemplateBatchMapper;
		}

		public string ProcessAndReturnId(TextTemplateModifyInputModel modifyInputModel)
		{
			var textTemplateBatch = textTemplateBatchModifyInputModelToTextTemplateBatchMapper.CreateInstance(modifyInputModel);
			textTemplateBatch.CreateDate = textTemplateBatch.CreateDate ?? DateTime.Now;
			textTemplateBatch.LastModifyDate = DateTime.Now;
			return textTemplateBatchRepository.SaveAndReturnId(textTemplateBatch);
		}
	}
}