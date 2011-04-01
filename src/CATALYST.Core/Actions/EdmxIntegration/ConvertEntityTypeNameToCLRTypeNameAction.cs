using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CATALYST.Core.Objects;

namespace CATALYST.Core.Actions.EdmxIntegration
{
	public class ConvertEntityTypeNameToCLRTypeNameAction
	{
		public string Execute(EntityProperty entityProperty)
		{
			if (entityProperty.TypeName == EntityProperty.TypeName_String) return "string";
			if (entityProperty.TypeName == EntityProperty.TypeName_Int32) return "int";
			if (entityProperty.TypeName == EntityProperty.TypeName_DateTime) return "System.DateTime";
			if (entityProperty.TypeName == EntityProperty.TypeName_Boolean) return "bool";
			if (entityProperty.TypeName == EntityProperty.TypeName_Binary) return "string";
			if (entityProperty.TypeName == EntityProperty.TypeName_Decimal) return "double";
			return null;
		}
	}
}
