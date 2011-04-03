using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.BatchProcessor.Models
{
	public class ZipBatch
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ZipFilename { get; set; }
	}
}