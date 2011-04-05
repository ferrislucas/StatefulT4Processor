using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.T4StateManager.Helpers
{
	public class GetValueFromTwoDimensionalStringArrayAsIfItWereAHash 
	{
		public string GetValue(string [][] data, string key)
		{
			foreach (var stringArray in data)
			{
				foreach (var s in stringArray)
				{
					if (s == key)
					{
						return stringArray[1];
					}
				}
				
			}
			return null;
		}
	}
}
