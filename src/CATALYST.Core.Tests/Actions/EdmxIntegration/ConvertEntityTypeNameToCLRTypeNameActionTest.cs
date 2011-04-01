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
	public class ConvertEntityTypeNameToCLRTypeNameActionTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_BinaryProperty_ReturnsString()
		{
			var convertEntityTypeNameToCLRTypeNameAction = mocker.Resolve<ConvertEntityTypeNameToCLRTypeNameAction>();

			var result = convertEntityTypeNameToCLRTypeNameAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_Binary });

			Assert.AreEqual("string", result);
		}

		[TestMethod]
		public void Execute_BooleanProperty_ReturnsBool()
		{
			var convertEntityTypeNameToCLRTypeNameAction = mocker.Resolve<ConvertEntityTypeNameToCLRTypeNameAction>();

			var result = convertEntityTypeNameToCLRTypeNameAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_Boolean });
			
			Assert.AreEqual("bool", result);
		}

		[TestMethod]
		public void Execute_DateTimeProperty_ReturnsDateTime()
		{
			var convertEntityTypeNameToCLRTypeNameAction = mocker.Resolve<ConvertEntityTypeNameToCLRTypeNameAction>();

			var result = convertEntityTypeNameToCLRTypeNameAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_DateTime });

			Assert.AreEqual("System.DateTime", result);
		}

		[TestMethod]
		public void Execute_Int32Property_ReturnsInt()
		{
			var convertEntityTypeNameToCLRTypeNameAction = mocker.Resolve<ConvertEntityTypeNameToCLRTypeNameAction>();

			var result = convertEntityTypeNameToCLRTypeNameAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_Int32 });

			Assert.AreEqual("int", result);
		}

		[TestMethod]
		public void Execute_StringProperty_ReturnsString()
		{
			var convertEntityTypeNameToCLRTypeNameAction = mocker.Resolve<ConvertEntityTypeNameToCLRTypeNameAction>();

			var result = convertEntityTypeNameToCLRTypeNameAction.Execute(new CATALYST.Core.Objects.EntityProperty() { TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual("string", result);
		}
	}
}
