using FluentValidation;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.Shared;

namespace StatefulT4Processor.DeploymentManager.Validators
{
	public class InputModelValidator : AbstractValidator<InputModel>
	{
		public InputModelValidator()
		{
			RuleFor(a => a.Name).Must(b => !string.IsNullOrEmpty(b)).WithMessage(ValidationMessages.FieldRequired);
		}
	}
}