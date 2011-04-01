using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StatefulT4Processor.Webroot.Models
{
	public class InputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		public string Name { get; set; }

		[DisplayName("Remote Name")]
		public string RemoteName { get; set; }
		[DisplayName("Branch Name")]
		public string BranchName { get; set; }
		[DisplayName("Push after building?")]
		public bool PushToRepository { get; set; }
		[DataType(DataType.MultilineText)]
		[DisplayName("Instruction Xml")]
		public string InstructionXml { get; set; }
		public string ProjectId { get; set; }
		public string Namespace { get; set; }
		public string Classname { get; set; }
		//public string PathToEntityDataModel { get; set; }
		public string ReplaceTag1 { get; set; }
		public string ReplaceTag2 { get; set; }
		public string ReplaceTag3 { get; set; }
		public string ReplaceTag4 { get; set; }
	}
}