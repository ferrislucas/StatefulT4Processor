using System;

namespace StatefulT4Processor.T4StateManager.Models
{
	[Serializable]
	public class T4ProcessState
	{
		public string Id { get; set; }
		public string Name { get; set;}

		public string RemoteName { get; set; }
		public string BranchName { get; set; }
		public bool PushToRepository { get; set; }
		public string InstructionXml { get; set; }
		public string ProjectId { get; set; }
		public string Namespace { get; set; }
		public string Classname { get; set; }
		public string PathToEntityDataModel { get; set; }
		public string ReplaceTag1 { get; set; }
		public string ReplaceTag2 { get; set; }
		public string ReplaceTag3 { get; set; }
		public string ReplaceTag4 { get; set; }
	}
}