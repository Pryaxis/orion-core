using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
	public class OrionModuleAttribute : Attribute
	{
        /// <summary>
        /// Gets or sets the module name.
        /// </summary>
		public string ModuleName { get; private set; }

        /// <summary>
        /// Gets or sets the module author.
        /// </summary>
		public string Author { get; private set; }
     
        /// <summary>
        /// (optional) Gets or sets the module description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// (optional) Gets or sets whether the module will be loaded with Orion or not.
        /// 
        /// Is enabled by default.
        /// </summary>
        public bool Enabled { get; set; } = true;
        
        /// <summary>
        /// (optional) Gets or sets the module version.  Defaults to whatever the Orion
        /// version is
        /// </summary>
        public Version ModuleVersion { get; set; } = typeof(Orion).Assembly.GetName().Version;

		public OrionModuleAttribute(string name, string author)
		{
			ModuleName = name;
			Author = author;
		}
	}
}
