using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Repositories;
using StatefulT4Processor.DeploymentManager.ViewModelBuilders;

namespace StatefulT4Processor.DeploymentManager.Tests.ViewModelBuilders
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
			mocker.GetMock<IWidgetRepository>()
				.Setup(a => a.GetAll())
				.Returns(new Deployment[]
				         	{
				         		new Deployment(), 
							});

			var result = mocker.Resolve<IndexViewModelBuilder>().BuildViewModel();

			Assert.AreEqual(1, result.Deployments.Count());
		}
	}
}
