using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapperAssist;
using StatefulT4Processor.TextTemplateBatchManager.Models;

namespace StatefulT4Processor.TextTemplateBatchManager.Mappers
{
	public interface ITextTemplateBatchToTextTemplateBatchModifyInputModelMapper
	{
		TextTemplateModifyInputModel CreateInstance(TextTemplateBatch source);
	}

	public class TextTemplateBatchToTextTemplateBatchModifyInputModelMapper
		: Mapper<Models.TextTemplateBatch, Models.TextTemplateModifyInputModel>, ITextTemplateBatchToTextTemplateBatchModifyInputModelMapper
	{
	}
}