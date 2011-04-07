using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using StatefulT4Processor.T4StateManager.Helpers;

namespace StatefulT4Processor.T4StateManager
{
	public interface IT4StateContext
	{
		void SetState(string xml);
		string GetStringFromState(string key);
		Dictionary<string, string> GetDictionaryFromState();
	}

	public class T4StateContext : IT4StateContext
	{
		private readonly GetValueFromTwoDimensionalStringArrayAsIfItWereAHash getValueFromTwoDimensionalStringArrayAsIfItWereAHash;
		private readonly GetDictionaryFromStringArray getDictionaryFromStringArray;

		public T4StateContext()
		{
			getValueFromTwoDimensionalStringArrayAsIfItWereAHash = new GetValueFromTwoDimensionalStringArrayAsIfItWereAHash();
			getDictionaryFromStringArray = new GetDictionaryFromStringArray();
		}

		public void SetState(string xml)
		{
			var path = GetPathToStateXmlFile();

			using (var file = new StreamWriter(path))
			{
				file.Write(xml);
			}
		}

		public string GetStringFromState(string key)
		{
			return getValueFromTwoDimensionalStringArrayAsIfItWereAHash.GetValue(GetState(), key);
		}

		public Dictionary<string, string> GetDictionaryFromState()
		{
			return getDictionaryFromStringArray.GetDictionaryFrom2DimensionalArray(GetState());
		}

		private string[][] GetState()
		{
			string fileContents;
			using (TextReader textReader = new StreamReader(GetPathToStateXmlFile()))
			{
				fileContents = textReader.ReadToEnd();
				textReader.Close();
			}

			using (var memoryStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(fileContents)))
			{
				var deserializer = new XmlSerializer(typeof(string[][]));
				var data = (string[][])deserializer.Deserialize(memoryStream);

				return data;
			}
		}

		private static string GetPathToStateXmlFile()
		{
			return @"C:\_Application\StatefulT4Processor\src\T4HostConsoleApplication\bin\Debug\state.xml";
			//return ConfigurationManager.AppSettings["PathToT4HostConsoleApplicationStateFile"];
			//return AppDomain.CurrentDomain.BaseDirectory
			//       + Path.DirectorySeparatorChar
			//       + "state.xml";
		}
	}
}
