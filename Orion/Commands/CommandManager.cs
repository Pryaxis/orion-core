using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Commands
{
    public class CommandManager
    {
        public List<Command> Commands { get;} = new List<Command>();

        private CommandStringParser _stringParser = new CommandStringParser();

        public void ParseAndCallCommand(OrionPlayer player, string commandString)
        {
            var name = CommandStringParser.GetCommandNameFromCommandString(commandString);
            var command = Commands.Single(x => x.CommandName == name);
            try
            {
                var argList = _stringParser.ParseCommandStringIntoArguments(commandString, command.ExpectedTypes);
                command.Call(argList);
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

        public void AddCommand(string name, Action commandMethod, object parent)
        {
            var comm = new Command(name, commandMethod.Method, parent);
            Commands.Add(comm);
        }

        #region AddCommand Overloads

        public void AddCommand<T>(string name, Action<T> commandMethod, object parent) where T : OrionPlayer
        {
            var comm = new Command(name, commandMethod.Method, parent);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2>(string name, Action<T1, T2> commandMethod, object parent) where T1 : OrionPlayer
        {
            var comm = new Command(name, commandMethod.Method, parent);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3>(string name, Action<T1, T2, T3> commandMethod, object parent) where T1 : OrionPlayer
        {
            var comm = new Command(name, commandMethod.Method, parent);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4>(string name, Action<T1, T2, T3, T4> commandMethod, object parent) where T1 : OrionPlayer
        {
            var comm = new Command(name, commandMethod.Method, parent);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4, T5>(string name, Action<T1, T2, T3, T4, T5> commandMethod, object parent) where T1 : OrionPlayer
        {
            var comm = new Command(name, commandMethod.Method, parent);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4, T5, T6>(string name, Action<T1, T2, T3, T4, T5, T6> commandMethod, object parent) where T1 : OrionPlayer
        {
            var comm = new Command(name, commandMethod.Method, parent);
            Commands.Add(comm);
        }

        #endregion
    }
}