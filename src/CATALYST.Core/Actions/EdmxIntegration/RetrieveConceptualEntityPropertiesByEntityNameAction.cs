using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CATALYST.Core.Actions.Config;
using System.Xml.Linq;
using CATALYST.Core.Objects;

namespace CATALYST.Core.Actions.EdmxIntegration
{
	public class RetrieveConceptualEntityPropertiesByEntityNameAction
	{
		private IGetPathToEdmxFileAction getPathToEdmxFileAction;

		public RetrieveConceptualEntityPropertiesByEntityNameAction(IGetPathToEdmxFileAction getPathToEdmxFileAction)
		{
			this.getPathToEdmxFileAction = getPathToEdmxFileAction;
		}

		public List<CATALYST.Core.Objects.EntityProperty> Execute(string entityName)
		{
			XNamespace xmlns = "http://schemas.microsoft.com/ado/2007/06/edmx";
			XNamespace ssdl = "http://schemas.microsoft.com/ado/2006/04/edm/ssdl";
			var filename = getPathToEdmxFileAction.Execute();
			var entities = from x in XElement.Load(filename)
									 .Descendants(xmlns + "ConceptualModels").Descendants().Descendants()
						   where (x.Name.LocalName == "EntityType")
						   select x;


			XNamespace edm = "http://schemas.microsoft.com/ado/2006/04/edm";
			var entityProperties = from item in entities.Where(a => a.Attribute("Name").Value == entityName).Descendants(edm + "Property")
								   select new EntityProperty() { Name = item.Attribute("Name").Value, TypeName = item.Attribute("Type").Value };

			return entityProperties
						.Where(a => a.Name != "Key")
						.Where(a => a.Name != "CreateDate")
						.Where(a => a.Name != "CreateBy")
						.Where(a => a.Name != "LastModifyDate")
						.Where(a => a.Name != "LastModifyBy")
					.ToList();
		}
	}
}
