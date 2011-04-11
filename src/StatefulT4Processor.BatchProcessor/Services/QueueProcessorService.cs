using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.Shared.Models;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface IQueueProcessorService
	{
		string[] ProcessQueue(Queue queue);
	}

	public class QueueProcessorService : IQueueProcessorService
	{
		public string[] ProcessQueue(Queue queue)
		{
			throw new NotImplementedException();
		}
	}
}
