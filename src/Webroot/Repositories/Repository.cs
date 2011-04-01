using System.Collections.Generic;
using EasyObjectStore.Helpers;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;

namespace StatefulT4Processor.Webroot.Repositories
{
	public interface IStateRepository
	{
		IEnumerable<T4ProcessState> GetAll();
		string SaveAndReturnId(T4ProcessState instance);
		void Delete(string id);
	}

	public class Repository : EasyObjectStore.EasyObjectStore<T4ProcessState>, IStateRepository
	{
		public Repository(IXmlFileSerializationHelper xmlFileSerializationHelper, IGetDataPathForType getDataPathForType, IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance, IGuidGetter guidGetter, IFileSystem fileSystem, ISetValueOfIdProperty setValueOfIdProperty) : base(xmlFileSerializationHelper, getDataPathForType, getValueOfIdPropertyForInstance, guidGetter, fileSystem, setValueOfIdProperty)
		{
		}
	}
}