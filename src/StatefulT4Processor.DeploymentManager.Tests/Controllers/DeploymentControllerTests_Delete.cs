using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.DeploymentManager.Controllers;
using StatefulT4Processor.DeploymentManager.Repositories;

namespace StatefulT4Processor.DeploymentManager.Tests.Controllers
{
	[TestClass]
	public class DeploymentControllerTests_Delete
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_redirect()
		{
			var result = mocker.Resolve<DeploymentManagerController>().Delete("id") as RedirectToRouteResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Calls_Delete_method_of_repository_with_id()
		{
			mocker.Resolve<DeploymentManagerController>().Delete("id");

			mocker.GetMock<IWidgetRepository>()
				.Verify(a => a.Delete("id"), Times.Once());
		}
	}
}
