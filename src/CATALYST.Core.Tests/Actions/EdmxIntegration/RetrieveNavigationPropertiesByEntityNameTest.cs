using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using CATALYST.Core.Actions.Config;
using CATALYST.Core.Actions.EdmxIntegration;
using CATALYST.Core.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CATALYST.Core.Tests.Actions.EdmxIntegration
{
	[TestClass]
	public class RetrieveNavigationPropertiesByEntityNameTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void GetProperties_Null_Returns2CorrectNavigationPropertyNames()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");

			var retrieveNavigationPropertiesByEntityName = mocker.Resolve<RetrieveNavigationPropertiesByEntityName>();
			var result = retrieveNavigationPropertiesByEntityName.GetProperties("TestEntity2");

			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.Where(a => a.Name == "TestEntity2Category").Count());
			Assert.AreEqual(1, result.Where(a => a.Name == "Faqs").Count());
		}

		[TestMethod]
		public void GetProperties_2NavigationProperties_Returns2CorrectNavigationPropertyTypes()
		{
			mocker.GetMock<IGetPathToEdmxFileAction>().Setup(a => a.Execute()).Returns(@"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Core.Tests\Actions\EdmxIntegration\Model1.edmx");

			var retrieveNavigationPropertiesByEntityName = mocker.Resolve<RetrieveNavigationPropertiesByEntityName>();
			var result = retrieveNavigationPropertiesByEntityName.GetProperties("TestEntity2");

			Assert.AreEqual(2, result.Where(a => a.TypeName == EntityProperty.TypeName_Navigation).Count());
		}


		[TestMethod]
		public void GetProperties_Null_ReturnsEmptySet()
		{
			var retrieveNavigationPropertiesByEntityName = mocker.Resolve<RetrieveNavigationPropertiesByEntityName>();
			var result = retrieveNavigationPropertiesByEntityName.GetProperties(null);

			Assert.AreEqual(0, result.Count());
		}
	}
}
