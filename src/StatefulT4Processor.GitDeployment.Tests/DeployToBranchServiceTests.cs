using System;
using System.Diagnostics;
using System.IO;
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
			var process = new Process
				{
					StartInfo =
					{
						FileName = @"C:\Program Files (x86)\Git\cmd\git.cmd", 
						Arguments = "clone git@github.com:ferrislucas/StatefulT4Processor.git",
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true,
						WorkingDirectory = @"C:\_Application\test"
					}
				};
			process.Start();
			process.WaitForExit();

			//var ouptput = LaunchEXE.Run(@"C:\_Application\test", @"C:\Program Files (x86)\Git\bin\git.exe", "clone git@github.com:ferrislucas/StatefulT4Processor.git", 60 * 20);

			//ExecuteGitCommand(@"C:\_Application\test", "git init");
			//ExecuteGitCommand(@"C:\_Application\test", "git remote add origin git@github.com:ferrislucas/StatefulT4Processor.git");
			//ExecuteGitCommand(@"C:\_Application\test", "git pull origin master");
			//ExecuteGitCommand(@"C:\_Application\test", "git pull origin test");
			//ExecuteGitCommand(@"C:\_Application\test", "git pull origin master");

			//var deployToBranchService = new DeployToBranchService(new x(), new FileSystem());
			//deployToBranchService.Deploy(new GitDeploymentTarget()
			//                                {
			//                                    BranchName = "test",
			//                                    RepositoryUrl = "git@github.com:ferrislucas/StatefulT4Processor.git",
			//                                }, @"C:\_Application\StatefulT4Processor\localWorkingFolder\T4Output\856d2587-5cf5-43a1-9fee-a2d4f67f16af");
		}


		public class LaunchEXE
		{
			public static string Run(string workingDirectory, string exeName, string argsLine, int timeoutSeconds)
			{
				StreamReader outputStream = StreamReader.Null;
				string output = "";
				bool success = false;

				try
				{
					Process newProcess = new Process();
					newProcess.StartInfo.FileName = exeName;
					newProcess.StartInfo.Arguments = argsLine;
					newProcess.StartInfo.UseShellExecute = false;
					newProcess.StartInfo.CreateNoWindow = false;
					newProcess.StartInfo.RedirectStandardOutput = true;
					newProcess.StartInfo.WorkingDirectory = workingDirectory;
					newProcess.Start();
					if (0 == timeoutSeconds)
					{
						outputStream = newProcess.StandardOutput;
						output = outputStream.ReadToEnd();
						newProcess.WaitForExit();
					}
					else
					{
						success = newProcess.WaitForExit(timeoutSeconds * 1000);

						if (success)
						{
							outputStream = newProcess.StandardOutput;
							output = outputStream.ReadToEnd();
						}
						else
						{
							output = "Timed out at " + timeoutSeconds + " seconds waiting for " + exeName + " to exit.";
						}
					}

				}
				catch (Exception e)
				{
					throw (new Exception("An error occurred running " + exeName + ".", e));
				}
				finally
				{
					outputStream.Close();
				}

				return "\t" + output;
			}
		}


		private string ExecuteGitCommand(string workingDirectory, string command)
		{
			if (command.StartsWith("git "))
				command = command.Substring(4);

			var process = new Process
			{
				StartInfo =
				{
					FileName = @"C:\Program Files (x86)\Git\bin\git.exe",
					Arguments = command,
					UseShellExecute = true,
					LoadUserProfile = true,
					
					RedirectStandardOutput = false,
					WorkingDirectory = workingDirectory,
				}
			};

			process.Start();

			process.WaitForExit();
			process.Close();
			process.Dispose();

			return null;
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
