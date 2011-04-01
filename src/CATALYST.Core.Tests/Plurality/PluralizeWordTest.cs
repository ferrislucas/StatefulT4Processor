using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.Plurality;

namespace CATALYST.Core.Tests.Plurality
{
	[TestClass]
	public class PluralizeWordTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_Category_ReturnsDogs()
		{
			var pluralizeWord = mocker.Resolve<PluralizeWord>();
			var result = pluralizeWord.Execute("category");

			Assert.AreEqual("categories", result);
		}

		[TestMethod]
		public void Execute_Dogs_ReturnsDogs()
		{
			var pluralizeWord = mocker.Resolve<PluralizeWord>();
			var result = pluralizeWord.Execute("dogs");

			Assert.AreEqual("dogs", result);
		}

		[TestMethod]
		public void Execute_Dog_ReturnsDogs()
		{
			var pluralizeWord = mocker.Resolve<PluralizeWord>();
			var result = pluralizeWord.Execute("dog");

			Assert.AreEqual("dogs", result);
		}

		[TestMethod]
		public void Execute_EmptyString_ReturnsEmptyString()
		{
			var pluralizeWord = mocker.Resolve<PluralizeWord>();
			var result = pluralizeWord.Execute(string.Empty);

			Assert.AreEqual(string.Empty, result);
		}

		[TestMethod]
		public void Execute_Null_ReturnsEmptyString()
		{
			var pluralizeWord = mocker.Resolve<PluralizeWord>();
			var result = pluralizeWord.Execute(null);

			Assert.AreEqual(string.Empty, result);
		}
	}
}
