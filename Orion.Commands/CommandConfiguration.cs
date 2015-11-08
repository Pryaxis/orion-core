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
        public List<char> FlagPrefixs { get; set; } = new List<char>{'/','-'};
    }
}
