using Orion.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion
{
    public partial class Orion
    {
        /// <summary>
        /// Gets the hook provider for this orion instance.  All hooks live here.
        /// </summary>
        public IHookProvider Hooks => Get<Modules.Hooks.OTAPIHookModule>();

        /// <summary>
        /// Gets the console provider for this orion instance.
        /// </summary>
        public IConsoleProvider ConsoleModule => Get<Modules.Console.ConsoleModule>();
        
        public IConfigurationProvider Configuration => Get<Modules.Configuration.ConfigurationModule>();

		public ILogProvider Log => Get<Modules.Logging.LogModule>();
    }
}
