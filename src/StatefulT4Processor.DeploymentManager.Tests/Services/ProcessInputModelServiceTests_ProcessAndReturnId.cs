using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

			mocker.GetMock<IWidgetRepository>()
				.Verify(a => a.SaveAndReturnId(It.Is<Deployment>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Returns_result_from_SaveAndReturnId_method_of_user_repository()
		{
			mocker.GetMock<IWidgetRepository>()
				.Setup(a => a.SaveAndReturnId(It.IsAny<Deployment>()))
				.Returns("test");

			var result = mocker.Resolve<ProcessInputModelService>().ProcessAndReturnId(new InputModel());

			Assert.AreEqual("test", result);
		}
	}
}
