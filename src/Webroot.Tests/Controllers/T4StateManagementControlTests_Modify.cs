using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Controllers;
using StatefulT4Processor.Webroot.Mappers;
using StatefulT4Processor.Webroot.Models;
using StatefulT4Processor.Webroot.Repositories;
using StatefulT4Processor.Webroot.ViewModelBuilders;

namespace Webroot.Tests.Controllers
{
	[TestClass]
	public class T4StateManagementControlTests_Modify
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
			var result = mocker.Resolve<T4StateManagementController>().Modify("id") as ViewResult;

			Assert.AreEqual("Modify", result.ViewName);
		}

		[TestMethod]
		public void Returns_view_from_view_model_builder()
		{
			mocker.GetMock<IModifyViewModelBuilder>().Setup(a => a.BuildViewModel(null))
				.Returns(new ModifyViewModel());

			var result = mocker.Resolve<T4StateManagementController>().Modify("id") as ViewResult;

			Assert.IsNotNull(result.ViewData.Model as ModifyViewModel);
		}

		[TestMethod]
		public void Passes_instance_from_repository_into_mapper()
		{
			mocker.GetMock<IStateRepository>().Setup(a => a.GetAll())
				.Returns(new T4ProcessState[]
				         	{
				         		new T4ProcessState()
				         			{
				         				Id = "test",
				         			}, 
							});

			mocker.Resolve<T4StateManagementController>().Modify("test");

			mocker.GetMock<IInstanceToWidgetInputModelMapper>()
				.Verify(a => a.CreateInstance(It.Is<T4ProcessState>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Passes_result_of_mapper_to_view_model_builder()
		{
			mocker.GetMock<IInstanceToWidgetInputModelMapper>().Setup(a => a.CreateInstance(It.IsAny<T4ProcessState>()))
				.Returns(new InputModel()
				         	{
				         		Id = "test"
				         	});
			mocker.GetMock<IStateRepository>().Setup(a => a.GetAll())
				.Returns(new T4ProcessState[]
				         	{
				         		new T4ProcessState()
				         			{
				         				Id = "test",
				         			}, 
							});

			mocker.Resolve<T4StateManagementController>().Modify("test");

			mocker.GetMock<IModifyViewModelBuilder>()
				.Verify(a => a.BuildViewModel(It.Is<InputModel>(b => b.Id == "test")), Times.Once());
		}
	}
}
