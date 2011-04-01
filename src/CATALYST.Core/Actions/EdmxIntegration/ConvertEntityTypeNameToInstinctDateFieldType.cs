using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CATALYST.Core.Objects;

namespace CATALYST.Core.Actions.EdmxIntegration
{
	public class ConvertEntityTypeNameToInstinctDateFieldType
	{
		public InstinctDataFieldType Execute(EntityProperty entityProperty)
		{
			if (entityProperty == null) return InstinctDataFieldType.Text;
			if (entityProperty.TypeName == EntityProperty.TypeName_String)
			{
				if (entityProperty.Name != null)
				{
					if (entityProperty.Name.ToLower().EndsWith("filename")) return InstinctDataFieldType.SimpleFileUploadProcessorUnit;
					if (entityProperty.Name.ToLower().EndsWith("file")) return InstinctDataFieldType.SimpleFileUploadProcessorUnit;
					if (entityProperty.Name.ToLower().EndsWith("image")) return InstinctDataFieldType.SimpleFileUploadProcessorUnit;
				}
				return InstinctDataFieldType.Text;
			}
			if (entityProperty.TypeName == EntityProperty.TypeName_DateTime) return InstinctDataFieldType.DateTime;
			if (entityProperty.TypeName == EntityProperty.TypeName_Int32) return InstinctDataFieldType.Integer;
			if (entityProperty.TypeName == EntityProperty.TypeName_Decimal) return InstinctDataFieldType.Money;
			if (entityProperty.TypeName == EntityProperty.TypeName_Money) return InstinctDataFieldType.Money;
			if (entityProperty.TypeName == EntityProperty.TypeName_Boolean) return InstinctDataFieldType.Bool;
			if (entityProperty.TypeName == EntityProperty.TypeName_Navigation) return InstinctDataFieldType.DropDown;
			return InstinctDataFieldType.Text;
		}
	}
}
