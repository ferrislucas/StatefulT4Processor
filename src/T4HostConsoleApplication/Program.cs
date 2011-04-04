using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using CATALYST.Core;

namespace T4HostConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			//Thread.Sleep(15000);
			
			var resultXml = string.Empty;
			try
			{
				var inputFile = args[0];
				var outputFile = args[1];

				var t4TemplateHostWrapper = new CATALYST.Core.T4TemplateHostWrapper();
				t4TemplateHostWrapper.ProcessT4File(inputFile, outputFile);

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
