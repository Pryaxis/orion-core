using System;
using System.Collections.Generic;

namespace Orion.Commands.Commands
{
    internal class RegisteredCommand
    {
        public string Name { get; private set; }
        public List<string> Permissions { get; private set; }
        public Type CommandClass { get; private set; }

        public RegisteredCommand(string name, List<string> permissions, Type commandClass)
        {
            Name = name;
            Permissions = permissions;
            CommandClass = commandClass;
        }
    }
}