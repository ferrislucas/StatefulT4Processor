using AutoMapperAssist;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;

namespace StatefulT4Processor.Webroot.Mappers
{
	public interface IInputModelToWidgetMapper
	{
		T4ProcessState CreateInstance(InputModel source);
	}

	public class InputModelToWidgetMapper : Mapper<InputModel, T4ProcessState>, IInputModelToWidgetMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<InputModel, T4ProcessState>()
					.ForMember(a => a.PathToEntityDataModel, b => b.Ignore())
				;
		}
	}
}