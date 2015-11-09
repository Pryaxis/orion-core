using System;

namespace Orion.Commands.Attributes
{
    public class CommandAttribute : Attribute
    {
        public string Name { get; set; }
        public string[] Permissions { get; set; }
        public string HelpText { get; set;}

        public CommandAttribute(string name)
        {
            Name = name;
        }

        public CommandAttribute(string name, params string[] permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        public CommandAttribute(string name, string helpText, params string[] permissions)
        {
            Name = name;
            Permissions = permissions;
            HelpText = helpText;
        }
    }
}