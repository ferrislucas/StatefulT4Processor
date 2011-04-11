using System;
using StatefulT4Processor.Shared.Models;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface ICreateQueueFromPathService
	{
		Queue RecursivelyBuildQueueFromPath(string path);
	}

	public class CreateQueueFromPathService : ICreateQueueFromPathService
	{
		public Queue RecursivelyBuildQueueFromPath(string path)
		{
			throw new NotImplementedException();
		}
	}
}
