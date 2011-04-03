using System.Collections.Generic;
using EasyObjectStore.Helpers;
using StatefulT4Processor.DeploymentManager.Models;

namespace StatefulT4Processor.DeploymentManager.Repositories
{
	public interface IWidgetRepository
	{
		IEnumerable<Deployment> GetAll();
		string SaveAndReturnId(Deployment instance);
		void Delete(string id);
	}

	public class Repository : EasyObjectStore.EasyObjectStore<Deployment>, IWidgetRepository
	{
		public Repository(IXmlFileSerializationHelper xmlFileSerializationHelper, IGetDataPathForType getDataPathForType, IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance, IGuidGetter guidGetter, IFileSystem fileSystem, ISetValueOfIdProperty setValueOfIdProperty) : base(xmlFileSerializationHelper, getDataPathForType, getValueOfIdPropertyForInstance, guidGetter, fileSystem, setValueOfIdProperty)
		{
		}
	}
}