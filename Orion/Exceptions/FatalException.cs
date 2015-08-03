using System;

namespace Orion.Exceptions
{
	public class FatalException : Exception
	{
		public FatalException(string message)
			: base(message)
		{
			//Should log message
			Environment.Exit(1);
		}
	}
}
