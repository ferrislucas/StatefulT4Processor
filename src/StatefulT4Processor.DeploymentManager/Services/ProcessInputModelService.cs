using StatefulT4Processor.DeploymentManager.Mappers;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Repositories;

namespace StatefulT4Processor.DeploymentManager.Services
{
	public interface IProcessInputModelService
	{
		string ProcessAndReturnId(InputModel userInputModel);
	}

	public class ProcessInputModelService : IProcessInputModelService
	{
		private readonly IInputModelToWidgetMapper inputModelToWidgetMapper;
		private readonly IWidgetRepository widgetRepository;

		public ProcessInputModelService(IInputModelToWidgetMapper inputModelToWidgetMapper,
										IWidgetRepository widgetRepository)
		{
			this.widgetRepository = widgetRepository;
			this.inputModelToWidgetMapper = inputModelToWidgetMapper;
		}

		public string ProcessAndReturnId(InputModel userInputModel)
		{
			return widgetRepository.SaveAndReturnId(inputModelToWidgetMapper.CreateInstance(userInputModel));
		}
	}
}