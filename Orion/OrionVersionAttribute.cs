using System;

namespace Orion
{
	/// <summary>
	/// Attribute for determining which version of Orion the plugin is compiled for
	/// </summary>
	public sealed class OrionVersionAttribute : Attribute
	{
		public Version version;

		public OrionVersionAttribute(Version version)
		{
			this.version = version;
		}

		public OrionVersionAttribute(int major, int minor) :
			this(new Version(major, minor))
		{
			
		}
	}
}
