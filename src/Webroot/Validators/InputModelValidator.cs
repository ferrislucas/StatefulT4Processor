using FluentValidation;
using StatefulT4Processor.Webroot.Models;

namespace StatefulT4Processor.Webroot.Validators
{
	public class InputModelValidator : AbstractValidator<InputModel>
	{
		public InputModelValidator()
		{
			RuleFor(a => a.Name).Must(b => !string.IsNullOrEmpty(b));
		}
	}
}