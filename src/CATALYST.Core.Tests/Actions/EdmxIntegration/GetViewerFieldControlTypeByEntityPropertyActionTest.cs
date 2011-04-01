using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.Actions.EdmxIntegration;
using CATALYST.Core.Objects;

namespace CATALYST.Core.Tests.Actions.EdmxIntegration
{
	[TestClass]
	public class GetViewerFieldControlTypeByEntityPropertyActionTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_BinaryPropertyTypeName_ReturnsFileUpload()
		{
			var getViewerFieldControlTypeByEntityPropertyAction = mocker.Resolve<GetViewerFieldControlTypeByEntityPropertyAction>();

			var result = getViewerFieldControlTypeByEntityPropertyAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_Binary });

			Assert.AreEqual(ViewerControlType.FileUpload, result);
		}

		[TestMethod]
		public void Execute_StringPropertyTypeNameAndPropertyNameIsBody_ReturnsHtmlTextBox()
		{
			var getViewerFieldControlTypeByEntityPropertyAction = mocker.Resolve<GetViewerFieldControlTypeByEntityPropertyAction>();

			var result = getViewerFieldControlTypeByEntityPropertyAction.Execute(new CATALYST.Core.Objects.EntityProperty() { Name = "Body", TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual(ViewerControlType.HtmlTextBox, result);
		}

		[TestMethod]
		public void Execute_StringPropertyTypeNameAndPropertyNameIsDescription_ReturnsHtmlTextBox()
		{
			var getViewerFieldControlTypeByEntityPropertyAction = mocker.Resolve<GetViewerFieldControlTypeByEntityPropertyAction>();

			var result = getViewerFieldControlTypeByEntityPropertyAction.Execute(new CATALYST.Core.Objects.EntityProperty() { Name = "Description", TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual(ViewerControlType.HtmlTextBox, result);
		}

		[TestMethod]
		public void Execute_StringPropertyTypeName_ReturnsTextBox()
		{
			var getViewerFieldControlTypeByEntityPropertyAction = mocker.Resolve<GetViewerFieldControlTypeByEntityPropertyAction>();

			var result = getViewerFieldControlTypeByEntityPropertyAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual(ViewerControlType.TextBox, result);
		}

		[TestMethod]
		public void Execute_BooleanPropertyTypeName_ReturnsCheckbox()
		{
			var getViewerFieldControlTypeByEntityPropertyAction = mocker.Resolve<GetViewerFieldControlTypeByEntityPropertyAction>();

			var result = getViewerFieldControlTypeByEntityPropertyAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_Boolean });

			Assert.AreEqual(ViewerControlType.CheckBox, result);
		}
	}
}
