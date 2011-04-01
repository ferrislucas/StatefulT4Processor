using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.Webroot.Mappers;

namespace Webroot.Tests.Mappers
{
	[TestClass]
	public class WidgetToUserInputModelMapperTests
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
