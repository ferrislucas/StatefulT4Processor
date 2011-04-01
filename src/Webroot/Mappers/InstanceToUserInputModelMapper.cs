using AutoMapperAssist;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;

namespace StatefulT4Processor.Webroot.Mappers
{
	public interface IInstanceToWidgetInputModelMapper
	{
		InputModel CreateInstance(T4ProcessState source);
	}

	public class InstanceToWidgetInputModelMapper : Mapper<T4ProcessState, Models.InputModel>, IInstanceToWidgetInputModelMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<T4ProcessState, Models.InputModel>()
				;
		}
	}
}