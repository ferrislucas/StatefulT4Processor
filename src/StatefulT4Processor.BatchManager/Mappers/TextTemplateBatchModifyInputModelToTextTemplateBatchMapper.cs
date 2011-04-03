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
				.ForMember(a => a.CreateDate, b => b.Ignore())
				.ForMember(a => a.LastModifyDate, b => b.Ignore());
		}

		public override TextTemplateBatch CreateInstance(TextTemplateModifyInputModel source)
		{
			var mappedInstance = base.CreateInstance(source);
			
			if (HttpContext.Current.Request.Files.AllKeys.Where(a => a == "ModifyInputModel_ZipFile").Any())
			{
				if (!string.IsNullOrEmpty(HttpContext.Current.Request.Files["ModifyInputModel_ZipFile"].FileName))
					mappedInstance.ZipFilename = HttpContext.Current.Request.Files["ModifyInputModel_ZipFile"].FileName;
			}
			
			return mappedInstance;
		}
	}
}