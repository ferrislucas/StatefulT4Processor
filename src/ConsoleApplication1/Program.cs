using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CATALYST.Core.Actions.FileSystem;
using CATALYST.Core.Objects;
using CATALYST.Core.CodeGenerationQueue;
using CATALYST.Core;
using CATALYST.Core.StateSerialization;
using EasyObjectStore.Helpers;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;
using StatefulT4Processor.Webroot.Models;
using FileSystem = CATALYST.Core.FileSystem;

namespace ConsoleApplication1
{
	class Program
	{
		private static T4ProcessState t4ProcessState;

		static void Main(string[] args)
		{
			InitializeState(args[0]);

			var errors = ConsumeQueue();

			DisplayErrorsAndWaitForKeystrokeIfNecessary(errors);
		}

		private static void InitializeState(string pathToState)
		{
			var xmlFileSerializationHelper = new XmlFileSerializationHelper();
			t4ProcessState = xmlFileSerializationHelper.DeserializeFromPath<T4ProcessState>(pathToState);

			new Core().SetState(t4ProcessState);
		}

		private static void DisplayErrorsAndWaitForKeystrokeIfNecessary(IEnumerable<string> errors)
		{
			foreach (var error in errors)
			{
				Console.WriteLine(error);
				Console.WriteLine(string.Empty);
			}
			//if (errors.Count() > 0) Console.ReadLine();
		}

		private static IEnumerable<string> ConsumeQueue()
		{
			var codeGenerationQueueConsumer = new CodeGenerationQueueConsumer(new T4TemplateHostWrapper(), new CreateDirectoryFromFilenameAction(new FileSystem()));
			return codeGenerationQueueConsumer.ExecuteAndReturnErrors(GenerateQueue());
		}

		public static IEnumerable<CodeGenerationQueueItem> GenerateQueue()
		{
			var configurationState = new ConfigurationState();
			var queue = new List<CodeGenerationQueueItem>();

			var createCodeGenerationQueueFromXml = new CreateCodeGenerationQueueFromXml(new Core(), new GetFileContents());
			queue.AddRange(createCodeGenerationQueueFromXml.Execute(t4ProcessState.InstructionXml));

			return queue;
		}

		public static T4ProcessState GetState()
		{
			var core = new CATALYST.Core.Core();
			return core.GetState();
		}
	}
}
