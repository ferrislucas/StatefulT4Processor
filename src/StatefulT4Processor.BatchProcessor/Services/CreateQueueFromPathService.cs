using System;
using System.Collections.Generic;
using System.Linq;
using StatefulT4Processor.Shared;
using StatefulT4Processor.Shared.Models;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface ICreateQueueFromPathService
	{
		Queue RecursivelyBuildQueueFromPath(string path);
	}

	public class CreateQueueFromPathService : ICreateQueueFromPathService
	{
		private readonly IFileSystem fileSystem;

		public CreateQueueFromPathService(IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public Queue RecursivelyBuildQueueFromPath(string path)
		{
			var queue = new Queue();
			queue.QueueItems = GetQueueItemsFromPath(path).ToArray();
			return queue;
		}

		public IEnumerable<QueueItem> GetQueueItemsFromPath(string path)
		{
			var list = new List<QueueItem>();
			foreach (var file in fileSystem.GetFiles(path).Where(a => a.EndsWith(".tt")))
			{
				list.Add(new QueueItem()
				         	{
				         		InputPath = file,
								OutputPath = file.Replace(".tt", string.Empty)
				         	});
			}
			foreach (var folder in fileSystem.GetDirectories(path))
			{
				list.AddRange(GetQueueItemsFromPath(folder));
			}
			return list;
		}
	}
}
