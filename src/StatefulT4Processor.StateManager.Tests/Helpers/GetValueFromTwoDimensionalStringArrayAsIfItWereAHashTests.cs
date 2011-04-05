using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.T4StateManager.Helpers;

namespace StatefulT4Processor.StateManager.Tests.Helpers
{
	[TestClass]
	public class GetValueFromTwoDimensionalStringArrayAsIfItWereAHashTests
	{
		private AutoMoqer mocker;
		private string[][] data = new string[][]
		                          	{
		                          		new string[] { "key1", "value1" }, 
										new string[] { "key2", "value2" }, 
									};

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_null_if_key_is_not_found()
		{
			var result = mocker.Resolve<GetValueFromTwoDimensionalStringArrayAsIfItWereAHash>().GetValue(data, "zzz");

			Assert.IsNull(result);
		}

		[TestMethod]
		public void Returns_correct_value_for_requested_key()
		{
			var result = mocker.Resolve<GetValueFromTwoDimensionalStringArrayAsIfItWereAHash>().GetValue(data, "key2");

			Assert.AreEqual("value2", result);
		}
	}
}
