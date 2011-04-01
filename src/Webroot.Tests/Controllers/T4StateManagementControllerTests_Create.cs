using System;
using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.Shared;
using StatefulT4Processor.Webroot.Controllers;
using StatefulT4Processor.Webroot.Models;

namespace Webroot.Tests.Controllers
{
	[TestClass]
	public class T4StateManagementControllerTests_Create
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
			var result = mocker.Resolve<T4StateManagementController>().Create() as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_model_with_id_property_of_UserInputModel_set_from_IGuidGetter()
		{
			var guid = Guid.NewGuid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid())
				.Returns(guid);

			var result = mocker.Resolve<T4StateManagementController>().Create() as ViewResult;

			var viewModel = result.ViewData.Model as ModifyViewModel;
			Assert.AreEqual(guid.ToString(), viewModel.InputModel.Id);
		}
	}
}
