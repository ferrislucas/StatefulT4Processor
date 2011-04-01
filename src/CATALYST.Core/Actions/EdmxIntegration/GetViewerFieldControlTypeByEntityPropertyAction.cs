using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CATALYST.Core.Objects;

namespace CATALYST.Core.Actions.EdmxIntegration
{
	public class GetViewerFieldControlTypeByEntityPropertyAction
	{
		public ViewerControlType Execute(EntityProperty entityProperty)
		{
			if (entityProperty.TypeName == EntityProperty.TypeName_Boolean)
			{
				return ViewerControlType.CheckBox;
			}
			if (entityProperty.TypeName == EntityProperty.TypeName_Binary) return ViewerControlType.FileUpload;
			if (entityProperty.Name == "Description") return ViewerControlType.HtmlTextBox;
			if (entityProperty.Name == "Body") return ViewerControlType.HtmlTextBox;
			return ViewerControlType.TextBox;
		}
	}
}
