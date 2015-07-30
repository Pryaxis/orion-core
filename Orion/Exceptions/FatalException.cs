using System;

namespace Orion.Exceptions
{
	public class FatalException : Exception
	{
		public FatalException(string message)
			: base(message)
		{
			Environment.Exit(1);
		}
	}
}
