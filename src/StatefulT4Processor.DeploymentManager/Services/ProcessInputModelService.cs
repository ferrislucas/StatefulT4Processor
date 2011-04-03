using StatefulT4Processor.DeploymentManager.Helpers;
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
		private readonly IDeploymentRepository deploymentRepository;
		private IGetCurrentDateTime getCurrentDateTime;

		public ProcessInputModelService(IInputModelToWidgetMapper inputModelToWidgetMapper,
										IDeploymentRepository deploymentRepository,
										IGetCurrentDateTime getCurrentDateTime)
		{
			this.getCurrentDateTime = getCurrentDateTime;
			this.deploymentRepository = deploymentRepository;
			this.inputModelToWidgetMapper = inputModelToWidgetMapper;
		}

		public string ProcessAndReturnId(InputModel userInputModel)
		{
			var deployment = inputModelToWidgetMapper.CreateInstance(userInputModel);
			deployment.CreateDate = deployment.CreateDate ?? getCurrentDateTime.Now();
			deployment.LastModifyDate = getCurrentDateTime.Now();
			return deploymentRepository.SaveAndReturnId(deployment);
		}
	}
}