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
        public CommandStringParser Parser { get; private set; }

        private List<RegisteredCommand> _registeredCommands = new List<RegisteredCommand>();
        private readonly CommandConfiguration _config;

        public CommandManager(CommandConfiguration config)
        {
            _config = config;
            Parser = new CommandStringParser(config.FlagPrefixs);
        }

        public void RegisterCommand<T>() where T : IOrionCommand, new()
        {
            Type commandType = typeof (T);
            CommandAttribute commandAttribute = commandType.GetCustomAttribute(typeof (CommandAttribute)) as CommandAttribute;
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
            string commandName = CommandStringParser.GetCommandNameFromCommandString(commandString, _config.CommandPrefix);

            if (commandName == "help")
            {
                HandleHelpCommand(player, commandString);
                return;
            }

            RegisteredCommand registeredCommand = _registeredCommands.Single(x => String.Equals(x.Name, commandName, StringComparison.CurrentCultureIgnoreCase));

            //Check perms here.

            IOrionCommand commandInstance = Parser.ParseArgumentsIntoCommandClass(registeredCommand.CommandClass, commandString, _config.FlagPrefixs);
            commandInstance.Sender = player;

            try
            {
                commandInstance.Run();
            }
            catch (Exception e)
            {
                //TODO: Proper exception handling and user feedback. Blocked by the possibility of @Wolfje making changes to output. Not filling in until then.
            }
        }

        private void HandleHelpCommand(BasePlayer player, string commandString)
        {
            //Split the help command string into several args.
            List<string> args = CommandStringParser.SplitCommandStringIntoArguments(commandString);
            var helpTextList = new List<string>();

            //Split each argument by '.' and provide the requested information.
            //Work on each argument separately to allow one call to the help command to return
            //info on multiple things.
            foreach (var arg in args)
            {
                string[] split = arg.Split('.');
                string commandName = split[0];

                RegisteredCommand command = _registeredCommands.Single(x => string.Equals(commandName, x.Name, StringComparison.CurrentCultureIgnoreCase));

                if (split.Length > 1)
                {
                    var subArg = split[1];
                    PropertyInfo[] props = command.CommandClass.GetProperties();
                    switch (subArg)
                    {
                        case "perms":
                        case "perm":
                        case "permissions":
                        case "permission":
                            helpTextList.Add($"{commandName} Permissions: {String.Join(", ", command.Permissions)}");
                            break;

                        default:
                            PropertyInfo prop = props.Single(x => string.Equals(subArg, x.Name, StringComparison.CurrentCultureIgnoreCase));
                            var attribute = prop.GetCustomAttributes().First(x => x is ParameterAttribute) as ParameterAttribute;
                            helpTextList.Add($"{commandName}.{prop.Name} - {attribute?.HelpText}");
                            break;
                    }
                }
                else
                {
                    CommandAttribute commandAttribute = command.CommandClass.GetCustomAttribute<CommandAttribute>();
                    helpTextList.Add($"{commandName} - {commandAttribute.HelpText}");
                }
            }

            //TODO: Output helpTextList to sender. Use pagination.
        }
    }
}