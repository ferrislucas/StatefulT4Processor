using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CATALYST.Core.Objects;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;

namespace CATALYST.Core.StateSerialization
{
	public interface ISerializeStateFromFile
	{
		T4ProcessState GetStateFromFile();
	}

	public class DeserializeStateFromFile : ISerializeStateFromFile
	{
		private IGetStateSerializationFilePath getStateSerializationFilePath;
		private IGetFileContents getFileContents;

		public DeserializeStateFromFile(IGetStateSerializationFilePath getStateSerializationFilePath, IGetFileContents getFileContents)
		{
			this.getStateSerializationFilePath = getStateSerializationFilePath;
			this.getFileContents = getFileContents;
		}

		public T4ProcessState GetStateFromFile()
		{
			var fileContents = getFileContents.Execute(getStateSerializationFilePath.GetPath());
			if (string.IsNullOrEmpty(fileContents)) return new T4ProcessState() { Classname = string.Empty, PathToEntityDataModel = string.Empty, Namespace = string.Empty, ProjectId = string.Empty, ReplaceTag1 = string.Empty, ReplaceTag2 = string.Empty, ReplaceTag3 = string.Empty, ReplaceTag4 = string.Empty };
			var xml = XDocument.Load(new System.IO.StringReader(fileContents));

			var query = from item in xml.Root.Descendants("T4ProcessState")
						select new T4ProcessState
						{
							ProjectId = item.Element("ProjectId").Value,
							Namespace = item.Element("Namespace").Value,
							Classname = item.Element("Classname").Value,
							PathToEntityDataModel = item.Element("PathToEntityDataModel").Value,
							ReplaceTag1 = item.Element("ReplaceTag1").Value,
							ReplaceTag2 = item.Element("ReplaceTag2").Value,
							ReplaceTag3 = item.Element("ReplaceTag3").Value,
							ReplaceTag4 = item.Element("ReplaceTag4").Value,
						};
			return query.First();
		}
	}
}
