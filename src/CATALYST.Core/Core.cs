using CATALYST.Core.Objects;
using CATALYST.Core.StateSerialization;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;

namespace CATALYST.Core
{
	public interface ICore
	{
		void SetState(T4ProcessState t4ProcessState);
		T4ProcessState GetState();
	}

	public class Core : ICore
	{
		private const string stateId = "T4ProcessState";
		
		public void SetState(T4ProcessState t4ProcessState)
		{
			//CallContext.SetData(stateId, T4ProcessState);
			var serializeStateToFile = new SerializeStateToFile(new GetStateSerializationFilePath());
			serializeStateToFile.Execute(t4ProcessState);
		}

		public T4ProcessState GetState() 
		{
			//var T4ProcessState = (IState)CallContext.GetData(stateId);
			var deserializer = new DeserializeStateFromFile(new GetStateSerializationFilePath(), new GetFileContents());
			var state = deserializer.GetStateFromFile();
			if (state == null) state = new T4ProcessState();// { ProjectId = "PROJECTID", Classname = "ENTITY" };
			return state;
		}
	}
}
