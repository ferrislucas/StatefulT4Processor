using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GitSharp;
using GitSharp.Commands;
using StatefulT4Processor.GitDeployment.Models;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;

namespace StatefulT4Processor.GitDeployment
{
	public class DeployToBranchService
	{
		private readonly IGetWorkingFolderPath getWorkingFolderPath;
		private IFileSystem fileSystem;

		public DeployToBranchService(IGetWorkingFolderPath getWorkingFolderPath,
									IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
			this.getWorkingFolderPath = getWorkingFolderPath;
		}

		public GitDeploymentResult Deploy(GitDeploymentTarget gitDeploymentTarget, string pathToGeneratedCode)
		{
			var tempPath = getWorkingFolderPath.GetPathToWorkingFolder() + this.GetType().Name + "Temp" + Path.DirectorySeparatorChar + Guid.NewGuid() + Path.DirectorySeparatorChar;

			var cloneCommand = new CloneCommand();
			cloneCommand.Source = gitDeploymentTarget.RepositoryUrl;

			cloneCommand.Directory = tempPath;
			cloneCommand.NoCheckout = true;
			cloneCommand.Execute();

			fileSystem.CopyFolder(pathToGeneratedCode, tempPath);

			var repo = new Repository(tempPath);
			GitSharp.Branch.Create(repo, "test_branch");

			//var currentCommit = repo.CurrentBranch.CurrentCommit;
			//repo.Index.AddAll();
			//repo.Commit("this is a test");)


			return null;
		}
	}
}
