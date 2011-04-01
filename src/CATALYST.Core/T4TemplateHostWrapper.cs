using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Mono.TextTemplating;

namespace CATALYST.Core
{
	public interface IT4TemplateHostWrapper
	{
		void ProcessT4File(string inputFile, string outputFile);
		IEnumerable<string> Errors {get; }
	}

	public class T4TemplateHostWrapper : IT4TemplateHostWrapper
	{
		private List<string> errors = new List<string>();

		public IEnumerable<string> Errors 
		{ 
			get 
			{
				//if (errors == null) errors = new List<string>();
				return errors;
			} 
		}

		public void ProcessT4File(string inputFile, string outputFile)
		{
			var templateGenerator = new TemplateGenerator();
			templateGenerator.ProcessTemplate(inputFile, outputFile);

			foreach (CompilerError item in templateGenerator.Errors)
			{
				errors.Add(item.ErrorText + ": " + item.FileName + " at line: " + item.Line);
			}
		}
	}
}
