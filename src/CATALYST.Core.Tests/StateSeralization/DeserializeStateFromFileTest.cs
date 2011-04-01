using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using CATALYST.Core.StateSerialization;
using Moq;

namespace CATALYST.Core.Tests.StateSeralization
{
	[TestClass]
	public class DeserializeStateFromFileTest
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectReplaceTag4()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("ReplaceTag4", result.ReplaceTag4);
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectReplaceTag3()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("ReplaceTag3", result.ReplaceTag3);
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectReplaceTag2()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("ReplaceTag2", result.ReplaceTag2);
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectReplaceTag1()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("ReplaceTag1", result.ReplaceTag1);
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectProjectId()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("ProjectId", result.ProjectId);
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectPathToEntityDataModel()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("PathToEntityDataModel", result.PathToEntityDataModel);
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectNamespace()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("Namespace", result.Namespace);
		}

		[TestMethod]
		public void GetStateFromFile_FileContainsValidSTate_ReturnsCorrectClassname()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(GetTestXml());

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual("Classname", result.Classname);
		}

		[TestMethod]
		public void GetStateFromFile_FileIsEmpty_ReturnsEmptyState()
		{
			mocker.GetMock<IGetFileContents>().Setup(a => a.Execute(It.IsAny<string>())).Returns(string.Empty);

			var serializeStateFromFile = mocker.Resolve<DeserializeStateFromFile>();
			var result = serializeStateFromFile.GetStateFromFile();

			Assert.AreEqual(string.Empty, result.Classname);
			Assert.AreEqual(string.Empty, result.Namespace);
			Assert.AreEqual(string.Empty, result.PathToEntityDataModel);
			Assert.AreEqual(string.Empty, result.ProjectId);
			Assert.AreEqual(string.Empty, result.ReplaceTag1);
			Assert.AreEqual(string.Empty, result.ReplaceTag2);
			Assert.AreEqual(string.Empty, result.ReplaceTag3);
			Assert.AreEqual(string.Empty, result.ReplaceTag4);
		}

		private string GetTestXml()
		{
			return @"<?xml version=""1.0"" ?>
				<Root>
				<T4ProcessState>
					<ProjectId>ProjectId</ProjectId>
					<Namespace>Namespace</Namespace>
					<Classname>Classname</Classname>
					<PathToEntityDataModel>PathToEntityDataModel</PathToEntityDataModel>
					<ReplaceTag1>ReplaceTag1</ReplaceTag1>
					<ReplaceTag2>ReplaceTag2</ReplaceTag2>
					<ReplaceTag3>ReplaceTag3</ReplaceTag3>
					<ReplaceTag4>ReplaceTag4</ReplaceTag4>
				</T4ProcessState>
				</Root>
			";
		}
	}
}
