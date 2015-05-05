using System;

namespace Orion
{
	public sealed class TemporaryAttribute : Attribute
	{
		public string Message { get; private set; }

		public TemporaryAttribute(string message )
		{
			Message = message;
		}
	}
}
