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
        public CommandStringParser Parser = new CommandStringParser();

        private List<RegisteredCommand> _registeredCommands = new List<RegisteredCommand>();
        private readonly CommandConfiguration _config;

        public CommandManager(CommandConfiguration config)
        {
            _config = config;
        }

        public void RegisterCommand<T>() where T : IOrionCommand, new()
        {
            var commandType = typeof (T);
            var commandAttribute = commandType.GetCustomAttribute(typeof (CommandAttribute)) as CommandAttribute;
            if (commandAttribute == null)
            {
                throw new CommandException("CommandAttribute not found.");
            }
            Assert.Expression(() => commandAttribute == null);

            if (_registeredCommands.Any(x => x.Name == commandAttribute.Name))
                throw new CommandException("A command with that name has already been registered.");
            _registeredCommands.Add(new RegisteredCommand(commandAttribute.Name, commandAttribute.Permissions.ToList(), commandType));
        }

        public void RunCommand(BasePlayer player, string commandString)
        {
            var commandName = CommandStringParser.GetCommandNameFromCommandString(commandString, _config.CommandPrefix);

            var registeredCommand = _registeredCommands.Single(x => String.Equals(x.Name, commandName, StringComparison.CurrentCultureIgnoreCase));

            //Check perms here.

            var commandInstance = Parser.ParseArgumentsIntoCommandClass(registeredCommand.CommandClass, commandString, _config.FlagPrefixs);

            try
            {
                commandInstance.Run();
            }
            catch (Exception e)
            {
                //TODO: Proper exception handling and user feedback.
            }
        }
    }
}