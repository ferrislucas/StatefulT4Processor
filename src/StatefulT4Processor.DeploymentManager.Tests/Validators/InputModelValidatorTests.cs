using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Validators;

namespace StatefulT4Processor.DeploymentManager.Tests.Validators
{
	[TestClass]
	public class InputModelValidatorTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_error_for_missing_Name()
		{
			var result = mocker.Resolve<InputModelValidator>().Validate(new InputModel());

			Assert.IsTrue(result.Errors.Where(a => a.PropertyName == "Name").Count() > 0);
		}
	}
}
