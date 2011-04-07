using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.T4StateManager.Helpers;

namespace StatefulT4Processor.T4StateManager.Tests
{
	[TestClass]
	public class GetDictionaryFromStringArrayTests
	{
		private AutoMoqer mocker;
		private string [][] array = new string[][]
											{
												new string[]{"token1", "value1"},
											};

		private string[][] badArray = new string[][]
											{
												new string[]{},
											};

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_dictionary()
		{
			var result = mocker.Resolve<GetDictionaryFromStringArray>().GetDictionaryFrom2DimensionalArray(array);

			Assert.AreEqual("token1", result.First().Key);
			Assert.AreEqual("value1", result.First().Value);
		}

		[TestMethod]
		public void Ignores_malformed_array()
		{
			var result = mocker.Resolve<GetDictionaryFromStringArray>().GetDictionaryFrom2DimensionalArray(badArray);

			Assert.AreEqual(0, result.Count);
		}

	}
}
