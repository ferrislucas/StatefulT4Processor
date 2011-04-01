using StatefulT4Processor.Webroot.Mappers;
using StatefulT4Processor.Webroot.Models;
using StatefulT4Processor.Webroot.Repositories;

namespace StatefulT4Processor.Webroot.Services
{
	public interface IProcessInputModelService
	{
		string ProcessAndReturnId(InputModel userInputModel);
	}

	public class ProcessInputModelService : IProcessInputModelService
	{
		private readonly IInputModelToWidgetMapper inputModelToWidgetMapper;
		private readonly IStateRepository stateRepository;

		public ProcessInputModelService(IInputModelToWidgetMapper inputModelToWidgetMapper,
										IStateRepository stateRepository)
		{
			this.stateRepository = stateRepository;
			this.inputModelToWidgetMapper = inputModelToWidgetMapper;
		}

		public string ProcessAndReturnId(InputModel userInputModel)
		{
			return stateRepository.SaveAndReturnId(inputModelToWidgetMapper.CreateInstance(userInputModel));
		}
	}
}