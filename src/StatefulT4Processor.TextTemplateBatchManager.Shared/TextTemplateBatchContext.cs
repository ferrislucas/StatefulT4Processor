using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatefulT4Processor.TextTemplateBatchManager.Models;

namespace StatefulT4Processor.TextTemplateBatchManager.Shared
{
	public interface ITextTemplateBatchContext
	{
		IEnumerable<TextTemplateBatch> GetAll();
	}
}
