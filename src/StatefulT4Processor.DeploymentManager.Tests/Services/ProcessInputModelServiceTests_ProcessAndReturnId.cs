using System;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.DeploymentManager.Helpers;
using StatefulT4Processor.DeploymentManager.Mappers;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Repositories;
using StatefulT4Processor.DeploymentManager.Services;

namespace StatefulT4Processor.DeploymentManager.Tests.Services
{
	[TestClass]
	public class ProcessInputModelServiceTests_ProcessAndReturnId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
			mocker.GetMock<IInputModelToWidgetMapper>().Setup(a => a.CreateInstance(It.IsAny<InputModel>())).Returns(new Deployment());
		}

		[TestMethod]
		public void Calls_SaveAndReturnId_method_of_user_repository_with_result_of_mapper()
		{
			mocker.GetMock<IInputModelToWidgetMapper>()
				.Setup(a => a.CreateInstance(It.Is<InputModel>(b => b.Id == "test")))
				.Returns(new Deployment()
				         	{
				         		Id = "test"
				         	});

			mocker.Resolve<ProcessInputModelService>()
				.ProcessAndReturnId(new InputModel()
				                    	{
				                    		Id = "test",
				                    	});

			mocker.GetMock<IDeploymentRepository>()
				.Verify(a => a.SaveAndReturnId(It.Is<Deployment>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Returns_result_from_SaveAndReturnId_method_of_user_repository()
		{
			mocker.GetMock<IDeploymentRepository>()
				.Setup(a => a.SaveAndReturnId(It.IsAny<Deployment>()))
				.Returns("test");

			var result = mocker.Resolve<ProcessInputModelService>().ProcessAndReturnId(new InputModel());

			Assert.AreEqual("test", result);
		}

		[TestMethod]
		public void Sets_CreateDate_to_current_date_time_when_saving_a_deployment_instance_with_a_null_CreateDate()
		{
			var now = DateTime.Now;
			mocker.GetMock<IGetCurrentDateTime>().Setup(a => a.Now())
				.Returns(now);
			mocker.GetMock<IInputModelToWidgetMapper>().Setup(a => a.CreateInstance(It.IsAny<InputModel>()))
				.Returns(new Deployment());
			
			mocker.Resolve<ProcessInputModelService>()
						.ProcessAndReturnId(new InputModel());

			mocker.GetMock<IDeploymentRepository>()
				.Verify(a => a.SaveAndReturnId(It.Is<Deployment>(b => b.CreateDate == now)), Times.Once());
		}

		[TestMethod]
		public void Does_not_set_CreateDate_to_current_date_time_when_saving_a_deployment_instance_with_CreateDate_set()
		{
			var now = DateTime.Now;
			mocker.GetMock<IGetCurrentDateTime>().Setup(a => a.Now())
				.Returns(now);
			mocker.GetMock<IInputModelToWidgetMapper>().Setup(a => a.CreateInstance(It.IsAny<InputModel>()))
				.Returns(new Deployment()
				         	{
				         		CreateDate = new DateTime(2010, 1, 1)
				         	});

			mocker.Resolve<ProcessInputModelService>()
						.ProcessAndReturnId(new InputModel());

			mocker.GetMock<IDeploymentRepository>()
				.Verify(a => a.SaveAndReturnId(It.Is<Deployment>(b => b.CreateDate == new DateTime(2010, 1, 1))), Times.Once());
		}

		[TestMethod]
		public void Sets_LastModifyDate_to_current_date_time()
		{
			var now = DateTime.Now;
			mocker.GetMock<IGetCurrentDateTime>().Setup(a => a.Now())
				.Returns(now);
			mocker.GetMock<IInputModelToWidgetMapper>().Setup(a => a.CreateInstance(It.IsAny<InputModel>()))
				.Returns(new Deployment());

			mocker.Resolve<ProcessInputModelService>()
						.ProcessAndReturnId(new InputModel());

			mocker.GetMock<IDeploymentRepository>()
				.Verify(a => a.SaveAndReturnId(It.Is<Deployment>(b => b.LastModifyDate == now)), Times.Once());
		}
	}
}
