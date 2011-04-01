using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CATALYST.Core.StateSerialization
{
	public interface IGetStateSerializationFilePath
	{
		string GetPath();
	}

	public class GetStateSerializationFilePath : IGetStateSerializationFilePath
	{
		public string GetPath()
		{
			return @"C:\_APPLICATION\CATALYST\T4ProcessState.xml";
		}
	}
}
