using System;

namespace Orion.Framework
{
	public class ServiceAttribute : Attribute
	{
		public string Name { get; set; }

		public string Author { get; set; }

		public Version Version { get; set; } = new Version(1, 0, 0);
	}
}
