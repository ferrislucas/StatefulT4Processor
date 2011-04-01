using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;
using StatefulT4Processor.Webroot.Repositories;
using StatefulT4Processor.Webroot.ViewModelBuilders;

namespace Webroot.Tests.ViewModelBuilders
{
	[TestClass]
	public class IndexViewModelBuilderTests_BuildViewModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_users_from_repository()
		{
			mocker.GetMock<IStateRepository>()
				.Setup(a => a.GetAll())
				.Returns(new T4ProcessState[]
				         	{
				         		new T4ProcessState(), 
							});

			var result = mocker.Resolve<IndexViewModelBuilder>().BuildViewModel();

			Assert.AreEqual(1, result.States.Count());
		}
	}
}
