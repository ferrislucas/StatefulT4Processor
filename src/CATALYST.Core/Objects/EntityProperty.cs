using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CATALYST.Core.Objects
{
	public class EntityProperty
	{
		public const string TypeName_Navigation = "TypeName_Navigation";
		public const string TypeName_Money = "Money";
		public const string TypeName_Binary = "Binary";
		public const string TypeName_Boolean = "Boolean";
		public const string TypeName_Int32 = "Int32";
		public const string TypeName_String = "String";
		public const string TypeName_DateTime = "DateTime";
		public const string TypeName_Decimal = "Decimal";

		public string Name { get; set; }
		public string TypeName { get; set; }
	}
}
