using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.Webroot.Controllers;
using StatefulT4Processor.Webroot.Repositories;

namespace Webroot.Tests.Controllers
{
	[TestClass]
	public class T4StateManagementControllerTests_Delete
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
			var result = mocker.Resolve<T4StateManagementController>().Delete("id") as RedirectToRouteResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Calls_Delete_method_of_repository_with_id()
		{
			mocker.Resolve<T4StateManagementController>().Delete("id");

			mocker.GetMock<IStateRepository>()
				.Verify(a => a.Delete("id"), Times.Once());
		}
	}
}
