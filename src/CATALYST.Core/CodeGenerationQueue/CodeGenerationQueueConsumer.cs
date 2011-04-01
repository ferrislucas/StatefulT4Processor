using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.TextTemplating;
using System.CodeDom.Compiler;
using CATALYST.Core.Actions.FileSystem;

namespace CATALYST.Core.CodeGenerationQueue
{
	public class CodeGenerationQueueConsumer
	{
		private IT4TemplateHostWrapper t4TemplateHostWrapper;
		private ICreateDirectoryFromFilenameAction createDirectoryFromFilenameAction;

		public CodeGenerationQueueConsumer(IT4TemplateHostWrapper t4TemplateHostWrapper, ICreateDirectoryFromFilenameAction createDirectoryFromFilenameAction)
		{
			this.t4TemplateHostWrapper = t4TemplateHostWrapper;
			this.createDirectoryFromFilenameAction = createDirectoryFromFilenameAction;
		}

		public IEnumerable<string> ExecuteAndReturnErrors(IEnumerable<CodeGenerationQueueItem> codeGenerationQueueItems)
		{
			var errors = new List<string>();

			if (codeGenerationQueueItems != null)
			{
				foreach (var codeGenerationQueueItem in codeGenerationQueueItems)
				{
					createDirectoryFromFilenameAction.Execute(codeGenerationQueueItem.OutputFilename);
					//Console.Write(string.Format("Processing file: {0}... ", codeGenerationQueueItem.InputFilename));
					t4TemplateHostWrapper.ProcessT4File(codeGenerationQueueItem.InputFilename, codeGenerationQueueItem.OutputFilename);
					//Console.WriteLine("done.");
				}
			}
			errors.AddRange(t4TemplateHostWrapper.Errors);
			return errors;
		}
	}
}