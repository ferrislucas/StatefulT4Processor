using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CATALYST.Core.Actions.Config;
using CATALYST.Core.Objects;

namespace CATALYST.Core.Actions.EdmxIntegration
{
	public interface IRetrieveNavigationPropertiesByEntityName
	{
		IEnumerable<EntityProperty> GetProperties(string entityName);
	}

	public class RetrieveNavigationPropertiesByEntityName : IRetrieveNavigationPropertiesByEntityName
	{
		private readonly IGetPathToEdmxFileAction getPathToEdmxFileAction;

		public RetrieveNavigationPropertiesByEntityName(IGetPathToEdmxFileAction getPathToEdmxFileAction)
		{
			this.getPathToEdmxFileAction = getPathToEdmxFileAction;
		}

		public IEnumerable<EntityProperty> GetProperties(string entityName)
		{
			if (string.IsNullOrEmpty(entityName)) return new List<EntityProperty>();
			
			XNamespace xmlns = "http://schemas.microsoft.com/ado/2007/06/edmx";
			XNamespace ssdl = "http://schemas.microsoft.com/ado/2006/04/edm/ssdl";
			var filename = getPathToEdmxFileAction.Execute();
			var entities = from x in XElement.Load(filename)
									 .Descendants(xmlns + "ConceptualModels").Descendants().Descendants()
						   where (x.Name.LocalName == "EntityType")
						   select x;

			XNamespace edm = "http://schemas.microsoft.com/ado/2006/04/edm";
			var entityProperties = from item in entities
									   .Where(a => a.Attribute("Name").Value == entityName).Descendants(edm + "NavigationProperty")
								   select new EntityProperty() { Name = item.Attribute("Name").Value, TypeName = EntityProperty.TypeName_Navigation };

			return entityProperties.ToList();
		}
	}
}
