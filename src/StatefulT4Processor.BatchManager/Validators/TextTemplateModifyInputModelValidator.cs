using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Models;

namespace StatefulT4Processor.TextTemplateBatchManager.Validators
{
	public class TextTemplateModifyInputModelValidator : AbstractValidator<TextTemplateModifyInputModel>
	{
		public TextTemplateModifyInputModelValidator()
		{
			RuleFor(a => a.Name).Must(b => !string.IsNullOrEmpty(b)).WithMessage(ValidationMessages.FieldRequired);
		}
	}
}