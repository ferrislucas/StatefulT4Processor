using System;

namespace StatefulT4Processor.TextTemplateBatchManager.Models
{
	public class TextTemplateBatch
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ZipFilename { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? LastModifyDate { get; set; }
	}
}