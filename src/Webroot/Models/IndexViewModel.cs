using System.Collections.Generic;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.T4StateManager.Models;

namespace StatefulT4Processor.Webroot.Models
{
	public class IndexViewModel
	{
		public IEnumerable<T4ProcessState> States { get; set; }
	}
}