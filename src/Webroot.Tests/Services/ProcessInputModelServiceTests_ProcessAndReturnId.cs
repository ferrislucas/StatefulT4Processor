using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Mappers;
using StatefulT4Processor.Webroot.Models;
using StatefulT4Processor.Webroot.Repositories;
using StatefulT4Processor.Webroot.Services;

namespace Webroot.Tests.Services
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
				.Returns(new T4ProcessState()
				         	{
				         		Id = "test"
				         	});

			mocker.Resolve<ProcessInputModelService>()
				.ProcessAndReturnId(new InputModel()
				                    	{
				                    		Id = "test",
				                    	});

			mocker.GetMock<IStateRepository>()
				.Verify(a => a.SaveAndReturnId(It.Is<T4ProcessState>(b => b.Id == "test")), Times.Once());
		}

		[TestMethod]
		public void Returns_result_from_SaveAndReturnId_method_of_user_repository()
		{
			mocker.GetMock<IStateRepository>()
				.Setup(a => a.SaveAndReturnId(It.IsAny<T4ProcessState>()))
				.Returns("test");

			var result = mocker.Resolve<ProcessInputModelService>().ProcessAndReturnId(new InputModel());

			Assert.AreEqual("test", result);
		}
	}
}
