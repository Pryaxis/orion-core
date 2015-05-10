using System;

namespace Orion
{
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
