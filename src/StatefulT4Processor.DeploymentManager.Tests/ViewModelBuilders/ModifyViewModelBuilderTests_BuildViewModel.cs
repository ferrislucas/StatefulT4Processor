using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.ViewModelBuilders;

namespace StatefulT4Processor.DeploymentManager.Tests.ViewModelBuilders
{
	[TestClass]
	public class ModifyViewModelBuilderTests_BuildViewModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_non_null_input_model_when_passed_null()
		{
			var result = mocker.Resolve<ModifyViewModelBuilder>()
											.BuildViewModel(null);

			Assert.IsNotNull(result.InputModel);
		}

		[TestMethod]
		public void Returns_input_model_passed_in()
		{
			var result = mocker.Resolve<ModifyViewModelBuilder>()
											.BuildViewModel(new InputModel()
											                	{
											                		Id = "id",
											                	});

			Assert.AreEqual("id", result.InputModel.Id);
		}
	}
}
