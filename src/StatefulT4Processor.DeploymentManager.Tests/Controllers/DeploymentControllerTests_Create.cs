using System;
using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.DeploymentManager.Controllers;
using StatefulT4Processor.DeploymentManager.Helpers;
using StatefulT4Processor.DeploymentManager.Models;

namespace StatefulT4Processor.DeploymentManager.Tests.Controllers
{
	[TestClass]
	public class DeploymentControllerTests_Create
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_view_name()
		{
			var result = mocker.Resolve<DeploymentManagerController>().Create() as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_model_with_id_property_of_UserInputModel_set_from_IGuidGetter()
		{
			var guid = Guid.NewGuid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid())
				.Returns(guid);

			var result = mocker.Resolve<DeploymentManagerController>().Create() as ViewResult;

			var viewModel = result.ViewData.Model as ModifyViewModel;
			Assert.AreEqual(guid.ToString(), viewModel.InputModel.Id);
		}
	}
}
