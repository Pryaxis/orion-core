using System;

namespace Orion
{
	public abstract class OrionModuleBase
	{
		public OrionModuleBase()
		{
			OrionModuleAttribute attrib = (OrionModuleAttribute)Attribute.GetCustomAttribute(GetType(), typeof(OrionModuleAttribute));

			if (attrib == null)
			{
				throw new Exception("Module does not display OrionModuleAttribute");
			}
		}

		public abstract void Run();
	}
}