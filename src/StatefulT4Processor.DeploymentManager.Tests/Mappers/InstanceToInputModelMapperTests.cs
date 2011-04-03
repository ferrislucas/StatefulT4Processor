using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.DeploymentManager.Mappers;

namespace StatefulT4Processor.DeploymentManager.Tests.Mappers
{
	[TestClass]
	public class InstanceToInputModelMapperTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Assert_configuration_is_valid()
		{
			var mapper = mocker.Resolve<InstanceToWidgetInputModelMapper>();
			mapper.AssertConfigurationIsValid();
		}
	}
}
