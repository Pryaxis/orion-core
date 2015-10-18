using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Orion.Commands.Commands.Exceptions;
using OTA;
using OTA.Command;

namespace Orion.Commands.Commands
{
    public class CommandManager
    {
        public List<Command> Commands { get;} = new List<Command>();

        public CommandStringParser Parser = new CommandStringParser();

        public void ParseAndCallCommand(BasePlayer player, string commandString)
        {
            var name = CommandStringParser.GetCommandNameFromCommandString(commandString);
            var args = CommandStringParser.SplitCommandStringIntoArguments(commandString);
            //Commands which match the amount of arguments and name of what the user input.
            var commands = Commands.Where(x => x.CommandName == name).Where(x => x.ExpectedTypes.Count == args.Count);

            //Command to fallback to by simply sending in an argument list.
            var argListFallback = Commands.FirstOrDefault(x => x.ExpectedTypes.Count == 1 && x.ExpectedTypes.First() == typeof(ArgumentList));

            var success = AttemptCommands(commands, commandString, player);
            if (!success)
            {
                if (argListFallback == null)
                {
                    //todo: feedback to player, command not found. log maybe?
                }
                else
                {
                    var argList = new ArgumentList();
                    argList.AddRange(args);
                    argListFallback.Call(new List<object>{argList});
                }
            }
        }

        /// <summary>
        /// Attempts run each provided command with the arguments provided. 
        /// Will attempt each command to determine if it is the correct command.
        /// </summary>
        /// <param name="commands">List of commands to try.</param>
        /// <param name="commandString">The command string as sent in by the user.</param>
        /// <returns>If a compatible command is found and succesfully run, returns true. Returns false upon total failure.</returns>
        private bool AttemptCommands(IEnumerable<Command> commands, string commandString, BasePlayer player)
        {
            foreach (var command in commands)
            {
                try
                {
                    var argList = Parser.ParseCommandStringIntoArguments(commandString, command.ExpectedTypes);
                    //Player is always the first argument unless the command is a variant that takes an ArgumentList.
                    argList.Insert(0, player);
                    command.Call(argList);
                    return true;
                }
                catch (Exception ex)
                {
                    //todo: log exception if in verbose mode
                }
            }
            return false;
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