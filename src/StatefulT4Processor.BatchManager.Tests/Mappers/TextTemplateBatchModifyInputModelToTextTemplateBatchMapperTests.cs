using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.TextTemplateBatchManager.Mappers;

namespace StatefulT4Processor.TextTemplateBatchManager.Tests.Mappers
{
	[TestClass]
	public class TextTemplateBatchModifyInputModelToTextTemplateBatchMapperTests
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
			var mapper = mocker.Resolve<TextTemplateBatchModifyInputModelToTextTemplateBatchMapper>();
			mapper.AssertConfigurationIsValid();
		}
	}
}
