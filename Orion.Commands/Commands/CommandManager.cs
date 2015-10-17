using System;
using System.Collections.Generic;
using System.Linq;
using Orion.Commands.Commands.Exceptions;
using OTA;
using OTA.Command;

namespace Orion.Commands.Commands
{
    public class CommandManager
    {
        //TODO: Add permission specification to each `AddCommand`
        /* 
            TODO: Allow multiple commands of the same name to be registered. Attempt to run the version of the command with the most compliant Action.
            Should also default to any versions of the command which take `ArgumentList` if available.
        */

        public List<Command> Commands { get;} = new List<Command>();

        public CommandStringParser Parser = new CommandStringParser();

        public void ParseAndCallCommand(BasePlayer player, string commandString)
        {
            var name = CommandStringParser.GetCommandNameFromCommandString(commandString);
            var args = CommandStringParser.SplitCommandStringIntoArguments(commandString);
            var commands = Commands.Where(x => x.CommandName == name).Where(x => x.ExpectedTypes.Count == args.Count);

            if (!commands.Any())
            {
                //TODO: Handle possibility of command not found or command variant not found.
            }


            foreach (var command in commands)
            {
                try
                {
                    var argList = Parser.ParseCommandStringIntoArguments(commandString, command.ExpectedTypes);
                    command.Call(argList);
                    return;
                }
                catch (ArgumentParsingException ex)
                {
                    //TODO: Message player here about error and log it.
                    throw;
                }
                catch (CommandException ex)
                {
                    //TODO: Message player here about error and log it.
                    throw;
                }
            }
        }

        public void AddCommand(string name, Action commandMethod)
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        #region AddCommand Overloads

        public void AddCommand<T>(string name, Action<T> commandMethod) where T : BasePlayer
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2>(string name, Action<T1, T2> commandMethod) where T1 : BasePlayer
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3>(string name, Action<T1, T2, T3> commandMethod) where T1 : BasePlayer
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4>(string name, Action<T1, T2, T3, T4> commandMethod) where T1 : BasePlayer
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4, T5>(string name, Action<T1, T2, T3, T4, T5> commandMethod) where T1 : BasePlayer
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4, T5, T6>(string name, Action<T1, T2, T3, T4, T5, T6> commandMethod) where T1 : BasePlayer
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        public void AddCommand(string name, Action<ArgumentList> commandMethod)
        {
            var comm = new Command(name, commandMethod.Method, commandMethod.Target);
            Commands.Add(comm);
        }

        #endregion
    }
}