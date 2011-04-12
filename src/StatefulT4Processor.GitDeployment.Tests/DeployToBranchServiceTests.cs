using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatefulT4Processor.GitDeployment.Models;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;

namespace StatefulT4Processor.GitDeployment.Tests
{
	[TestClass]
	public class DeployToBranchServiceTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			var deployToBranchService = new DeployToBranchService(new x(), new FileSystem());
			deployToBranchService.Deploy(new GitDeploymentTarget()
			                             	{
			                             		BranchName = "test",
												//RepositoryUrl = "https://ferrislucas@github.com/ferrislucas/EasyObjectStore.git", 
												RepositoryUrl = "git://github.com/ferrislucas/EasyObjectStore.git",
												//RepositoryUrl = "git@degsapp07.deg.local:Test2.git",
											}, @"C:\_Application\StatefulT4Processor\localWorkingFolder\T4Output\856d2587-5cf5-43a1-9fee-a2d4f67f16af");
		}
	}

	public class x : IGetWorkingFolderPath
	{
		public string GetPathToWorkingFolder()
		{
			return @"C:\_Application\StatefulT4Processor\localWorkingFolder\";
		}
	}
}
