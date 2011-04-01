using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using CATALYST.Core.Actions.EdmxIntegration;
using CATALYST.Core.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CATALYST.Core.Tests.Actions.EdmxIntegration
{
	[TestClass]
	public class ConvertEntityTypeNameToInstinctDateFieldTypeTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_NavigationEntityProperty_ReturnsDropDownDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_Navigation });

			Assert.AreEqual(InstinctDataFieldType.DropDown, result);
		}

		[TestMethod]
		public void Execute_Null_ReturnsTextDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(null);

			Assert.AreEqual(InstinctDataFieldType.Text, result);
		}

		[TestMethod]
		public void Execute_UnknownEntityProperty_ReturnsTextDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { Name = "zzzFile", TypeName = "xxxxxxxxx" });

			Assert.AreEqual(InstinctDataFieldType.Text, result);
		}

		[TestMethod]
		public void Execute_StringEntityPropertyWithNameThatEndsInFILENAME_ReturnsSimpleFileUploadProcessorUnitDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { Name = "zzzFilename", TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual(InstinctDataFieldType.SimpleFileUploadProcessorUnit, result);
		}
	
		[TestMethod]
		public void Execute_StringEntityPropertyWithNameThatEndsInFILE_ReturnsSimpleFileUploadProcessorUnitDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { Name = "zzzFile", TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual(InstinctDataFieldType.SimpleFileUploadProcessorUnit, result);
		}

		[TestMethod]
		public void Execute_StringEntityPropertyWithNameThatEndsInIMAGE_ReturnsSimpleFileUploadProcessorUnitDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { Name = "zzzImage", TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual(InstinctDataFieldType.SimpleFileUploadProcessorUnit, result);
		}

		[TestMethod]
		public void Execute_BooleanEntityProperty_ReturnsBooleanDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_Boolean});

			Assert.AreEqual(InstinctDataFieldType.Bool, result);
		}

		[TestMethod]
		public void Execute_MoneyEntityProperty_ReturnsMoneyDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_Money });

			Assert.AreEqual(InstinctDataFieldType.Money, result);
		}

		[TestMethod]
		public void Execute_DecimalEntityProperty_ReturnsMoneyDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_Decimal });

			Assert.AreEqual(InstinctDataFieldType.Money, result);
		}

		[TestMethod]
		public void Execute_Int32EntityProperty_ReturnsIntegerDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_Int32 });

			Assert.AreEqual(InstinctDataFieldType.Integer, result);
		}

		[TestMethod]
		public void Execute_DateTimeEntityProperty_ReturnsDateTimeDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_DateTime});

			Assert.AreEqual(InstinctDataFieldType.DateTime, result);
		}

		[TestMethod]
		public void Execute_StringEntityProperty_ReturnsTextDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_String});

			Assert.AreEqual(InstinctDataFieldType.Text, result);
		}

		[TestMethod]
		public void Execute_EntityPropertyWithNullName_ReturnsTextDataFieldType()
		{
			var convertEntityTypeNameToInstinctDateFieldType = mocker.Resolve<ConvertEntityTypeNameToInstinctDateFieldType>();
			var result = convertEntityTypeNameToInstinctDateFieldType.Execute(new EntityProperty() { TypeName = EntityProperty.TypeName_String });

			Assert.AreEqual(InstinctDataFieldType.Text, result);
		}
	}
}
