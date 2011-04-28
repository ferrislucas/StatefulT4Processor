<Query Kind="Statements">
  <Reference Relative="lib\gitsharp\GitSharp.Core.dll">C:\_Application\StatefulT4Processor\lib\gitsharp\GitSharp.Core.dll</Reference>
  <Reference Relative="lib\gitsharp\GitSharp.dll">C:\_Application\StatefulT4Processor\lib\gitsharp\GitSharp.dll</Reference>
  <Reference Relative="src\StatefulT4Processor.Shared\bin\Debug\StatefulT4Processor.Shared.dll">C:\_Application\StatefulT4Processor\src\StatefulT4Processor.Shared\bin\Debug\StatefulT4Processor.Shared.dll</Reference>
  <Namespace>GitSharp</Namespace>
  <Namespace>GitSharp.Commands</Namespace>
  <Namespace>StatefulT4Processor.Shared</Namespace>
</Query>

var pathToRepo = @"C:\_Application\StatefulT4Processor\localWorkingFolder\test";
var gitUrl = "git@github.com:ferrislucas/StatefulT4Processor.git";
Git.Clone(gitUrl, pathToRepo);

var repo = new Repository(pathToRepo);
var branch = GitSharp.Branch.Create(repo, "test_branch");
branch.Checkout();

var fileSystem = new FileSystem();
fileSystem.CopyFolder(@"C:\_Application\StatefulT4Processor\localWorkingFolder\T4Output\856d2587-5cf5-43a1-9fee-a2d4f67f16af", pathToRepo);

foreach (var untrackedItem in repo.Status.Untracked)
{
	Console.WriteLine("adding untracked item: " + untrackedItem);
	repo.Index.Add(untrackedItem);
}

foreach (var removedItem in repo.Status.Removed)
{
	Console.WriteLine("removing removed item: " + removedItem);
	repo.Index.Remove(removedItem);
}

var commit = repo.Commit("commit " + Guid.NewGuid().ToString(), new Author("ferris", "ferris.lucas@gmail.com"));


var process = new Process
				{
					StartInfo =
					{
						FileName = @"C:\Program Files (x86)\Git\bin\git.exe",
						Arguments = "push origin " + branch.Name,
						UseShellExecute = false,
						RedirectStandardOutput = true,
					}
				};
process.Start();
