using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace StatefulT4Processor.TextTemplateZipProcessor.Helpers
{
	public interface IT4TemplateHostWrapper
	{
		void ProcessT4File(string inputFile, string outputFile);
		IEnumerable<string> Errors { get; }
	}

	public class T4TemplateHostWrapper : IT4TemplateHostWrapper
	{
		private List<string> errors = new List<string>();

		public void ProcessT4File(string inputFile, string outputFile)
		{
			var p = new Process
			{
				StartInfo =
				{
					FileName = ConfigurationManager.AppSettings["PathToT4HostConsoleApplication"],
					Arguments = string.Format("{0} {1}", inputFile, outputFile),
					UseShellExecute = false,
					RedirectStandardOutput = true
				}
			};
			//p.StartInfo.Arguments = p.StartInfo.Arguments + " " + p.Id;
			p.Start();
			
			var consoleOutput = p.StandardOutput.ReadToEnd();

			using (var memoryStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(consoleOutput)))
			{
				var deserializer = new XmlSerializer(typeof(string[]));
				var data = (string[]) deserializer.Deserialize(memoryStream);
				errors.AddRange(data);
			}
		}

		public IEnumerable<string> Errors
		{
			get { return errors; }
		}
	}
}
