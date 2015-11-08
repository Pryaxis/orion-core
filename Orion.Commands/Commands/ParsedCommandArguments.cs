using System.Collections.Generic;

namespace Orion.Commands.Commands
{
    public class ParsedCommandArguments
    {
        public Dictionary<string, string> Flags = new Dictionary<string, string>();
        public List<string> Switches = new List<string>();
        public List<string> PositionalArguments = new List<string>();

        public bool GetSwitch(string sw)
        {
            return Switches.Contains(sw);
        }
    }
}