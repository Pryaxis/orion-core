using OTA.Logging;
using OTA.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion
{
    public class OrionPlugin : BasePlugin
    {
        public OrionPlugin() 
            : base()
        {
            Author = "Nyx Studios";
            Description = "Plugin that exposes a comprehensive API for TShock v5+";
            Version = this.GetType().Assembly.GetName().Version.ToString();
        }

        protected override void Initialized(object state)
        {
            Version orionVersion = this.GetType().Assembly.GetName().Version;
            ProgramLog.Log($"Orion version {orionVersion.ToString()} initialized.");
        }
    }
}
