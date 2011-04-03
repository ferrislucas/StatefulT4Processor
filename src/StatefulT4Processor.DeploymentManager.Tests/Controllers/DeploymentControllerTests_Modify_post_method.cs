using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.DeploymentManager.Controllers;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Services;
using StatefulT4Processor.DeploymentManager.ViewModelBuilders;

namespace StatefulT4Processor.DeploymentManager.Tests.Controllers
{
	[TestClass]
	public class DeploymentControllerTests_Modify_post_method
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_view_name_when_model_state_is_not_valid()
		{
			var controller = mocker.Resolve<DeploymentManagerController>();
			controller.ModelState.AddModelError("key", "error");

			var result = controller.Modify(new InputModel()) as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_from_view_model_builder_when_model_state_is_not_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<InputModel>()))
				.Returns(new ModifyViewModel());
			var controller = mocker.Resolve<DeploymentManagerController>();
			controller.ModelState.AddModelError("key", "error");

			var result = controller.Modify((InputModel) null) as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_input_model_into_view_model_builder_when_model_state_is_not_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<InputModel>()))
				.Returns(new ModifyViewModel());
			var controller = mocker.Resolve<DeploymentManagerController>();
			controller.ModelState.AddModelError("key", "error");

			controller.Modify(new InputModel()
			                                        	{
			                                        		Name = "test",
			                                        	});

			mocker.GetMock<IModifyViewModelBuilder>()
				.Verify(a => a.BuildViewModel(It.Is<InputModel>(b => b.Name == "test")), Times.Once());
		}

		[TestMethod]
		public void Calls_ProcessUserInputModelService_when_model_state_is_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<InputModel>()))
				.Returns(new ModifyViewModel());

			mocker.Resolve<DeploymentManagerController>().Modify(new InputModel()
			                                        	{
			                                        		Id = "test"
			                                        	});

			mocker.GetMock<IProcessInputModelService>()
				.Verify(a => a.ProcessAndReturnId(It.Is<InputModel>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Returns_redirect_to_Modify_action_when_model_state_is_valid()
		{
			mocker.GetMock<IModifyViewModelBuilder>()
				.Setup(a => a.BuildViewModel(It.IsAny<InputModel>()))
				.Returns(new ModifyViewModel());

			var result = mocker.Resolve<DeploymentManagerController>().Modify(new InputModel()) as RedirectToRouteResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Does_not_call_ProcessUserInputModelService_when_model_state_is_not_valid()
		{
			var controller = mocker.Resolve<DeploymentManagerController>();
			controller.ModelState.AddModelError("key", "error");

			controller.Modify(new InputModel());

			mocker.GetMock<IProcessInputModelService>()
				.Verify(a => a.ProcessAndReturnId(It.IsAny<InputModel>()), Times.Never());
		}
	}
}
