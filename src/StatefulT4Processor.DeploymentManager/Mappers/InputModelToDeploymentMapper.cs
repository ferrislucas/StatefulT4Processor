using AutoMapperAssist;
using StatefulT4Processor.DeploymentManager.Models;

namespace StatefulT4Processor.DeploymentManager.Mappers
{
	public interface IInputModelToWidgetMapper
	{
		Deployment CreateInstance(InputModel source);
	}

	public class InputModelToDeploymentMapper : Mapper<InputModel, Deployment>, IInputModelToWidgetMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<InputModel, Deployment>()
					.ForMember(a => a.CreateDate, b=> b.Ignore())
					.ForMember(a => a.LastModifyDate, b => b.Ignore())
				;
		}
	}
}