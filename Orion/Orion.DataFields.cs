using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
