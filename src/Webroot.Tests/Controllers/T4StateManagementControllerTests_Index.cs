using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.Webroot.Controllers;
using StatefulT4Processor.Webroot.Models;
using StatefulT4Processor.Webroot.ViewModelBuilders;

namespace Webroot.Tests.Controllers
{
	[TestClass]
	public class T4StateManagementControllerTests_Index
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
			var result = mocker.Resolve<T4StateManagementController>().Index() as ViewResult;

			Assert.AreEqual("Index", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_from_view_model_builder()
		{
			mocker.GetMock<IIndexViewModelBuilder>()
				.Setup(a => a.BuildViewModel())
				.Returns(new IndexViewModel());

			var result = mocker.Resolve<T4StateManagementController>().Index() as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as IndexViewModel);
		}
	}
}
