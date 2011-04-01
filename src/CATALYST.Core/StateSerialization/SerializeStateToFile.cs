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
	public class SerializeStateToFile
	{
		private IGetStateSerializationFilePath getStateSerializationFilePath;

		public SerializeStateToFile(IGetStateSerializationFilePath getStateSerializationFilePath)
		{
			this.getStateSerializationFilePath = getStateSerializationFilePath;
		}

		public void Execute(T4ProcessState t4ProcessState)
		{
			XDocument doc = new
						XDocument(new
						XDeclaration("1.0",
										  "utf-8", "yes"),
				new XElement("Root",
					new XElement("T4ProcessState",
						new XElement("ProjectId", t4ProcessState.ProjectId),
						new XElement("Namespace", t4ProcessState.Namespace),
						new XElement("Classname", t4ProcessState.Classname),
						new XElement("PathToEntityDataModel", t4ProcessState.PathToEntityDataModel),
						new XElement("ReplaceTag1", t4ProcessState.ReplaceTag1),
						new XElement("ReplaceTag2", t4ProcessState.ReplaceTag2),
						new XElement("ReplaceTag3", t4ProcessState.ReplaceTag3),
						new XElement("ReplaceTag4", t4ProcessState.ReplaceTag4)
					)
				)
			);
			doc.Save(getStateSerializationFilePath.GetPath());
		}
	}
}
