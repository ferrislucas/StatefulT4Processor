using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CATALYST.Core.Plurality
{
	public class PluralizeWord
	{
		public string Execute(string word)
		{
			if (string.IsNullOrEmpty(word)) return string.Empty;
			if (word.EndsWith("s") == false)
			{
				if (word.EndsWith("y")) word = word.Substring(0, word.Length - 1) + "ies";
				else word = word + "s";
			}
			return word;
		}
	}
}
