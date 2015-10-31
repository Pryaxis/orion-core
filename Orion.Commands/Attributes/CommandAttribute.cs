using System;

namespace Orion.Commands.Attributes
{
    public class CommandAttribute : Attribute
    {
        public string Name { get; set; }
        public string[] Permissions { get; set; }

        public CommandAttribute(string Name, params string[] permissions)
        {
            this.Name = Name;
            Permissions = permissions;
        }
    }
}