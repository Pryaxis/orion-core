using System;

namespace Orion.Configuration
{
	public abstract class BaseConfig
	{
		/// <summary>
		/// Called whenever the config file is read
		/// </summary>
		/// <param name="baseCfg"></param>
		public abstract void OnRead(BaseConfig baseCfg);
	}
}
