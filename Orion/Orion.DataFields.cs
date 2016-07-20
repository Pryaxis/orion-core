namespace Orion
{
	public partial class Orion
	{
		public static string PluginDirectory => "plugins";

		/// <summary>
		/// A list of directories that must exist under orion's directory
		/// </summary>
		protected string[] standardDirectories = new[]
		{
			PluginDirectory
		};


	}
}
