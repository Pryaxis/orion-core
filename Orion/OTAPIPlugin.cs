using OTA.Logging;
using OTA.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion
{
    /// <summary>
    /// Defines the entry point for orion, and how it interacts with OTAPI
    /// </summary>
    [OTAVersion(1,0)]
    public class OTAPIPlugin : BasePlugin
    {
        protected Orion orionInstance;

        public OTAPIPlugin() 
            : base()
        {
            Author = "Nyx Studios";
            Description = "Plugin that exposes a comprehensive API for TShock v5+";
            Version = this.GetType().Assembly.GetName().Version.ToString();

            orionInstance = new Orion(this);
        }

        /// <summary>
        /// Occurs when the plugin is initialized.
        /// </summary>
        /// <param name="state"></param>
        protected override void Initialized(object state)
        {
            Version orionVersion = this.GetType().Assembly.GetName().Version;
            orionInstance.Initialize();
            //ProgramLog.Log($"Orion version {orionVersion.ToString()} initialized.");
        }

        protected override void Disposed(object state)
        {
            orionInstance.Dispose();
        }
    }
}
