using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.TextTemplateBatchManager.Contexts;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.Repositories;

namespace StatefulT4Processor.TextTemplateBatchManager.Tests.Contexts
{
	[TestClass]
	public class TextTemplateBatchContextTests_GetAll
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_text_template_batches_from_repository()
		{
			mocker.GetMock<ITextTemplateBatchRepository>()
				.Setup(a => a.GetAll())
				.Returns(new TextTemplateBatch[]
				         	{
				         		new TextTemplateBatch()
				         			{
				         				Id = "test",
				         			}, 
							});

			var result = mocker.Resolve<TextTemplateBatchContext>().GetAll();

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("test", result.First().Id);
		}
	}
}
