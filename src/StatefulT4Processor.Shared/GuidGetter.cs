using System;

namespace StatefulT4Processor.Shared
{
	public interface IGuidGetter
	{
		Guid GetGuid();
	}

	public class GuidGetter : IGuidGetter
	{
		public Guid GetGuid()
		{
			return Guid.NewGuid();
		}
	}
}
