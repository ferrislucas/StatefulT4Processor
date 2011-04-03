using AutoMapperAssist;
using StatefulT4Processor.DeploymentManager.Models;

namespace StatefulT4Processor.DeploymentManager.Mappers
{
	public interface IInstanceToWidgetInputModelMapper
	{
		InputModel CreateInstance(Deployment source);
	}

	public class InstanceToWidgetInputModelMapper : Mapper<Models.Deployment, Models.InputModel>, IInstanceToWidgetInputModelMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<Models.Deployment, Models.InputModel>()
				;
		}
	}
}