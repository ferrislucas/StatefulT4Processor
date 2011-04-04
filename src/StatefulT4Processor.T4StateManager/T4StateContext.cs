using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StatefulT4Processor.T4StateManager
{
	public interface IT4StateContext
	{
		void SetState(string xml);
		string GetStringFromState(string key);
	}

	public class T4StateContext : IT4StateContext
	{
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
			string fileContents;
			using (TextReader textReader = new StreamReader(GetPathToStateXmlFile()))
			{
				fileContents = textReader.ReadToEnd();
				textReader.Close();	
			}

			using (var memoryStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(fileContents)))
			{
				var deserializer = new XmlSerializer(typeof(SerializableDictionary<string, string>));
				var data = (SerializableDictionary<string, string>)deserializer.Deserialize(memoryStream);

				return data[key];
			}
		}

		private static string GetPathToStateXmlFile()
		{
			return AppDomain.CurrentDomain.BaseDirectory
				   + Path.DirectorySeparatorChar
				   + "state.xml";
		}
	}
}
