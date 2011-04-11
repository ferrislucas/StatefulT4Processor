using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using CATALYST.Core;
using StatefulT4Processor.Shared.Models;

namespace T4HostConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var resultXml = string.Empty;
			try
			{
				Queue queue;
				var queueFile = args[0];

				string fileContents;
				using (TextReader textReader = new StreamReader(queueFile))
				{
					fileContents = textReader.ReadToEnd();
					textReader.Close();
				}
				using (var memoryStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(fileContents)))
				{
					var deserializer = new XmlSerializer(typeof(Queue));
					queue = (Queue)deserializer.Deserialize(memoryStream);
				}

				var t4TemplateHostWrapper = new T4TemplateHostWrapper();
				
				foreach (var item in queue.QueueItems)
				{
					t4TemplateHostWrapper.ProcessT4File(item.InputPath, item.OutputPath);
				}
				
				resultXml = GetResultXml(t4TemplateHostWrapper);
			}
			catch (Exception e)
			{
				resultXml = SerializeExceptionToXml(e);
			}

			Console.WriteLine(resultXml);
		}

		private static string SerializeExceptionToXml(Exception e)
		{
			string resultXml;
			using (var memoryStream = new MemoryStream())
			{
				var serializer = new XmlSerializer(typeof(string[]));
				serializer.Serialize(memoryStream, new string[]{ e.Message, e.StackTrace });
				memoryStream.Position = 0;
				var sr = new StreamReader(memoryStream);
				resultXml = sr.ReadToEnd();
			}
			return resultXml;
		}

		private static string GetResultXml(T4TemplateHostWrapper t4TemplateHostWrapper)
		{
			var resultXml = string.Empty;
			using (var memoryStream = new MemoryStream())
			{
				var serializer = new XmlSerializer(typeof(string[]));
				serializer.Serialize(memoryStream, t4TemplateHostWrapper.Errors.ToArray());
				memoryStream.Position = 0;
				var sr = new StreamReader(memoryStream);
				resultXml = sr.ReadToEnd();
			}
			return resultXml;
		}
	}
}
