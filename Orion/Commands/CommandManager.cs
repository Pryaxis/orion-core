using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Commands
{
    public class CommandManager
    {
        public List<Command> Commands { get;} = new List<Command>();

        private CommandParser Parser = new CommandParser();

        public void ParseAndCallCommand(OrionPlayer player, string commandString)
        {
            var name = CommandParser.GetCommandNameFromCommandString(commandString);
            var command = Commands.Single(x => x.CommandName == name);
            var argList = Parser.ParseCommand(commandString, command.ExpectedTypes);
            try
            {
                command.Call(argList);
            }
            catch (Exception ex)
            {
                //TODO: Handle command exceptions here.
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