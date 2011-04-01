using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CATALYST.Core.StateSerialization;

namespace CATALYST.Core.CodeGenerationQueue
{
	public class CreateCodeGenerationQueueFromXml
	{
		private ICore core;
		private IGetFileContents getFileContents;

		public CreateCodeGenerationQueueFromXml(ICore core, IGetFileContents getFileContents)
		{
			this.core = core;
			this.getFileContents = getFileContents;
		}

		public IEnumerable<CodeGenerationQueueItem> Execute(string codeGenerationQueueXml)
		{
			if (string.IsNullOrEmpty(codeGenerationQueueXml)) return new CodeGenerationQueueItem[]{};
		
			//var fileContents = getFileContents.Execute(xml);
			//if (string.IsNullOrEmpty(fileContents) == true) return new List<CodeGenerationQueueItem>();

			var xml = XDocument.Load(new System.IO.StringReader(codeGenerationQueueXml));
			var query = from item in xml.Root.Elements("T4File")
						select new CodeGenerationQueueItem { InputFilename = item.Attribute("InputFilename").Value, OutputFilename = item.Attribute("OutputFilename").Value };

			return ProcessReplaceTags(query);
		}

		private IEnumerable<CodeGenerationQueueItem> ProcessReplaceTags(IEnumerable<CodeGenerationQueueItem> codeGenerationQueueItems)
		{
			var result = new List<CodeGenerationQueueItem>();
			var state = core.GetState();
			foreach (var item in codeGenerationQueueItems)
			{
				result.Add(new CodeGenerationQueueItem() { InputFilename = item.InputFilename, OutputFilename = item.OutputFilename.Replace("::Classname::", state.Classname)} ); 
			}

			return result;
		}
	}
}
