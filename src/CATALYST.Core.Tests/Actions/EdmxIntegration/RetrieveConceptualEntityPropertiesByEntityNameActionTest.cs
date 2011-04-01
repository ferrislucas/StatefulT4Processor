using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.Actions;
using CATALYST.Core.Actions.EdmxIntegration;
using CATALYST.Core.Actions.Config;
using CATALYST.Core.Objects;

namespace CATALYST.Core.Tests.Actions
{
	[TestClass]
	public class RetrieveConceptualEntityPropertiesByEntityNameActionTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_FiltersOutLastModifyByProperty()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(0, result.Where(a => a.Name == "LastModifyBy").Count());
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_FiltersOutLastModifyDateProperty()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(0, result.Where(a => a.Name == "LastModifyDate").Count());
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_FiltersOutCreateByProperty()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(0, result.Where(a => a.Name == "CreateBy").Count());
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_FiltersOutCreateDateProperty()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(0, result.Where(a => a.Name == "CreateDate").Count());
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_FiltersOutKeyProperty()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(0, result.Where(a => a.Name == "Key").Count());
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_ReturnsCorrectTypeField()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(1, result.Where(a => a.Name == "Name").Where(a => a.TypeName == EntityProperty.TypeName_String).Count());
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_ReturnsCorrectNameField()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(1, result.Where(a => a.Name == "Name").Count());
		}

		[TestMethod]
		public void Execute_PropertiesOnTestEntity_ReturnsCorrectNumberOfProperties()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");
			var retrieveConceptualEntityPropertiesByEntityNameAction = mocker.Resolve<RetrieveConceptualEntityPropertiesByEntityNameAction>();

			var result = retrieveConceptualEntityPropertiesByEntityNameAction.Execute("TestEntityDoNotAlter");

			Assert.AreEqual(8, result.Count());
		}
	}
}
