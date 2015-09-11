namespace Orion.Configuration
{
	public abstract class BaseConfig
	{
		/// <summary>
		/// This controls what happens when your config file is read.
		/// The <see cref="BaseConfig"></see> provided by this method can be cast
		/// to your config's type. It should then be used to update the values in your config.
		/// See <see cref="ConfigFile"></see> for an example
		/// </summary>
		/// <param name="baseCfg"></param>
		public abstract void OnRead(BaseConfig baseCfg);
	}
}
