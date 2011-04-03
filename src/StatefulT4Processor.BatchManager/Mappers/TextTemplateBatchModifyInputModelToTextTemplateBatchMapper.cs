using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapperAssist;
using StatefulT4Processor.TextTemplateBatchManager.Models;

namespace StatefulT4Processor.TextTemplateBatchManager.Mappers
{
	public interface ITextTemplateBatchModifyInputModelToTextTemplateBatchMapper
	{
		TextTemplateBatch CreateInstance(TextTemplateModifyInputModel source);
	}

	public class TextTemplateBatchModifyInputModelToTextTemplateBatchMapper
		: Mapper<TextTemplateModifyInputModel, TextTemplateBatch>, ITextTemplateBatchModifyInputModelToTextTemplateBatchMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<Models.TextTemplateModifyInputModel, Models.TextTemplateBatch>()
				.ForMember(a => a.ZipFilename, b => b.Ignore());
		}

		public override TextTemplateBatch CreateInstance(TextTemplateModifyInputModel source)
		{
			var mappedInstance = base.CreateInstance(source);
			var files = HttpContext.Current.Request.Files;
			//mappedInstance.ZipFilename = HttpContext.Current.Request.Files[""]
			return mappedInstance;
		}
	}
}