using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatefulT4Processor.TextTemplateBatchManager.Models
{
	public class IndexViewModel
	{
		public IEnumerable<TextTemplateBatch> TextTemplateBatches { get; set; }
	}
}