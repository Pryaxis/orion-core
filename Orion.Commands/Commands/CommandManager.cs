using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Orion.Commands.Attributes;
using Orion.Commands.Commands.Exceptions;
using OTA;
using OTA.DebugFramework;

namespace Orion.Commands.Commands
{
    public class CommandManager
    {
        private List<RegisteredCommand> _registeredCommands = new List<RegisteredCommand>(); 

        public void RegisterCommand<T>() where T : IOrionCommand, new()
        {
            var commandType = typeof (T);
            var commandAttribute = commandType.GetCustomAttribute(typeof (CommandAttribute)) as CommandAttribute;
            if (commandAttribute == null)
            {
                throw new CommandException("CommandAttribute not found.");
            }
            Assert.Expression(() => commandAttribute == null);
            _registeredCommands.Add(new RegisteredCommand(commandAttribute.Name, commandAttribute.Permissions.ToList(), commandType));
        }

        public void RunCommand(BasePlayer player, string commandString)
        {
            
        }
    }
}