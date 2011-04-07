using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatefulT4Processor.T4StateManager.Helpers
{
	public class GetDictionaryFromStringArray
	{
		public Dictionary<string, string> GetDictionaryFrom2DimensionalArray(string[][] array)
		{
			var dictionary = new Dictionary<string, string>();
			foreach (var item in array)
			{
				if (item.Count() == 2)
					dictionary.Add(item[0], item[1]);
			}
			return dictionary;
		}
	}
}
