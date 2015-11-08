using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Commands
{
    public class CommandConfiguration
    {
        public string CommandPrefix { get; set; } = "/";
        public List<string> FlagPrefix { get; set; } = new List<string>{"/","-"};
    }
}
