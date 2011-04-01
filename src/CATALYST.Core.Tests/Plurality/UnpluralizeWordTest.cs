using AutoMoq;
using CATALYST.Core.Plurality;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CATALYST.Core.Tests.Plurality
{
	[TestClass]
	public class UnpluralizeWordTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Execute_WordIsNotPlural_ReturnsEmptyString()
		{
			var unpluralizeWord = mocker.Resolve<UnpluralizeWord>();
			var result = unpluralizeWord.Execute("category");

			Assert.AreEqual("category", result);
		}

		[TestMethod]
		public void Execute_WordEndsInIes_ReturnsEmptyString()
		{
			var unpluralizeWord = mocker.Resolve<UnpluralizeWord>();
			var result = unpluralizeWord.Execute("categories");

			Assert.AreEqual("category", result);
		}

		[TestMethod]
		public void Execute_WordEndsInS_ReturnsEmptyString()
		{
			var unpluralizeWord = mocker.Resolve<UnpluralizeWord>();
			var result = unpluralizeWord.Execute("dogs");

			Assert.AreEqual("dog", result);
		}

		[TestMethod]
		public void Execute_Null_ReturnsEmptyString()
		{
			var unpluralizeWord = mocker.Resolve<UnpluralizeWord>();
			var result = unpluralizeWord.Execute(null);

			Assert.AreEqual(string.Empty, result);
		}

		[TestMethod]
		public void Execute_EmptyString_ReturnsEmptyString()
		{
			var unpluralizeWord = mocker.Resolve<UnpluralizeWord>();
			var result = unpluralizeWord.Execute(string.Empty);

			Assert.AreEqual(string.Empty, result);
		}

	}
}
