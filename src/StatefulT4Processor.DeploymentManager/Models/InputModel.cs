using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StatefulT4Processor.DeploymentManager.Models
{
	public class InputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }
		public string Name { get; set; }

		[DisplayName("Batch")]
		[UIHint("TextTemplateBatchId")]
		public string TextTemplateBatchId { get; set; }

		[DisplayName("State")]
		[DataType(DataType.MultilineText)]
		public string StateXml { get; set; }
	}
}