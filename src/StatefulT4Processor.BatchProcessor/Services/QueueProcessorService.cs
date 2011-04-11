using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using StatefulT4Processor.Shared.Models;

namespace StatefulT4Processor.TextTemplateZipProcessor.Services
{
	public interface IQueueProcessorService
	{
		string[] ProcessQueue(Queue queue);
	}

	public class QueueProcessorService : IQueueProcessorService
	{
		public string[] ProcessQueue(Queue queue)
		{
			string xml;
			using (var memoryStream = new MemoryStream())
			{
				var serializer = new XmlSerializer(typeof(string[]));
				serializer.Serialize(memoryStream, queue);
				memoryStream.Position = 0;
				var sr = new StreamReader(memoryStream);
				xml = sr.ReadToEnd();
			}

			var path = string.Format("{0}QueueProcessorWorkingFolder" + Path.DirectorySeparatorChar + "{1}" + Path.DirectorySeparatorChar, ConfigurationManager.AppSettings["PathToLocalWorkingFolder"], Guid.NewGuid());
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			
			var pathToQueueXmlFile = path + "queue.xml";
			using (var file = new StreamWriter(pathToQueueXmlFile))
			{
				file.Write(xml);
				file.Close();
			}

			var p = new Process
			{
				StartInfo =
				{
					FileName = ConfigurationManager.AppSettings["PathToT4HostConsoleApplication"],
					Arguments = string.Format("{0}", pathToQueueXmlFile),
					UseShellExecute = false,
					RedirectStandardOutput = true,
				}
			};
			p.Start();

			var consoleOutput = p.StandardOutput.ReadToEnd();

			return consoleOutput.Split('\n');
		}
	}
}
