using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatefulT4Processor.TextTemplateBatchManager.Models
{
	public class ModifyInputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		public string Name { get; set; }

		[DisplayName("Zip File")]
		public string ZipFile { get; set; }
	}
}