using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Framework
{
    public class ServiceAttribute : Attribute
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public Version Version { get; set; } = new Version(1, 0, 0);
    }
}
