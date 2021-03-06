﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
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
		private readonly IFileSystem fileSystem;

		public DeployToBranchService(IGetWorkingFolderPath getWorkingFolderPath,
									IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
			this.getWorkingFolderPath = getWorkingFolderPath;
		}

		public GitDeploymentResult Deploy(GitDeploymentTarget gitDeploymentTarget, string pathToGeneratedCode)
		{
			var tempPath = getWorkingFolderPath.GetPathToWorkingFolder() + this.GetType().Name + "Temp" + Path.DirectorySeparatorChar + Guid.NewGuid() + Path.DirectorySeparatorChar;
			fileSystem.CreateFolder(tempPath);

			CloneTheRepositoryAndGetOnTheDesiredBranch(tempPath, gitDeploymentTarget);

			fileSystem.CopyFolder(pathToGeneratedCode, tempPath);

			AddTheNewFilesAndCommitThem(tempPath);

			ExecuteGitCommand(tempPath, string.Format("git push origin {0}", gitDeploymentTarget.BranchName));

			return null;
		}

		private void AddTheNewFilesAndCommitThem(string tempPath)
		{
			ExecuteGitCommand(tempPath, "git add --all");
			ExecuteGitCommand(tempPath, "git commit -am \'auto_generated_commit\'");
		}

		private void CloneTheRepositoryAndGetOnTheDesiredBranch(string tempPath, GitDeploymentTarget gitDeploymentTarget)
		{
			ExecuteGitCommand(tempPath, string.Format("git clone {0} ./", gitDeploymentTarget.RepositoryUrl));
			if (ExecuteGitCommand(tempPath, "git branch").Contains(gitDeploymentTarget.BranchName))
			{
				ExecuteGitCommand(tempPath, string.Format("git checkout {0}", gitDeploymentTarget.BranchName));
			} else
			{
				ExecuteGitCommand(tempPath, string.Format("git checkout -b {0}", gitDeploymentTarget.BranchName));
			}
		}

		private string ExecuteGitCommand(string workingDirectory, string command)
		{
			var unixStylePathToUserHomeDirectory = ConfigurationManager.AppSettings["UnixStylePathToUserHomeDirectory"];
			var pathToGitShellExe = ConfigurationManager.AppSettings["PathToGitShellExe"];
			var pathToGitShellScript = ConfigurationManager.AppSettings["PathToGitShellScript"];

			if (command.StartsWith("git "))
				command = command.Substring(4);

			var process = new Process
			{
				StartInfo =
				{
					FileName = pathToGitShellExe,
					Arguments = pathToGitShellScript + " " + unixStylePathToUserHomeDirectory + @" """ + command + @"""",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true,
					WorkingDirectory = workingDirectory,
				}
			};
			process.Start();
			process.WaitForExit();

			var output = process.StandardOutput.ReadToEnd();

			process.Close();
			process.Dispose();

			return output;
		}
	}
}
