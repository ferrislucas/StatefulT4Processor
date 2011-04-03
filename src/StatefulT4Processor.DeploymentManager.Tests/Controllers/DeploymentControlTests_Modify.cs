using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.DeploymentManager.Controllers;
using StatefulT4Processor.DeploymentManager.Mappers;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Repositories;
using StatefulT4Processor.DeploymentManager.ViewModelBuilders;

namespace StatefulT4Processor.DeploymentManager.Tests.Controllers
{
	[TestClass]
	public class DeploymentControlTests_Modify
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
			var result = mocker.Resolve<DeploymentManagerController>().Modify("id") as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_from_view_model_builder()
		{
			mocker.GetMock<IModifyViewModelBuilder>().Setup(a => a.BuildViewModel(null))
				.Returns(new ModifyViewModel());

			var result = mocker.Resolve<DeploymentManagerController>().Modify("id") as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_instance_from_repository_into_mapper()
		{
			mocker.GetMock<IWidgetRepository>().Setup(a => a.GetAll())
				.Returns(new Deployment[]
				         	{
				         		new Deployment()
				         			{
				         				Id = "test",
				         			}, 
							});

			mocker.Resolve<DeploymentManagerController>().Modify("test");

			mocker.GetMock<IInstanceToWidgetInputModelMapper>()
				.Verify(a => a.CreateInstance(It.Is<Deployment>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Passes_result_of_mapper_to_view_model_builder()
		{
			mocker.GetMock<IInstanceToWidgetInputModelMapper>().Setup(a => a.CreateInstance(It.IsAny<Deployment>()))
				.Returns(new InputModel()
				         	{
				         		Id = "test"
				         	});
			mocker.GetMock<IWidgetRepository>().Setup(a => a.GetAll())
				.Returns(new Deployment[]
				         	{
				         		new Deployment()
				         			{
				         				Id = "test",
				         			}, 
							});

			mocker.Resolve<DeploymentManagerController>().Modify("test");

			mocker.GetMock<IModifyViewModelBuilder>()
				.Verify(a => a.BuildViewModel(It.Is<InputModel>(b => b.Id == "test")), Times.Once());
		}
	}
}
