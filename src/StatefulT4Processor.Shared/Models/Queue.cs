using System.Collections.Generic;

namespace StatefulT4Processor.Shared.Models
{
	public class Queue
	{
		public IEnumerable<QueueItem> QueueItems { get; set; }
	}

	public class QueueItem
	{
		public string InputPath { get; set; }
		public string OutputPath { get; set; }
	}
}
