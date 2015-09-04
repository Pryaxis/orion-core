using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Commands
{
    public class CommandManager
    {
        private List<Command> Commands = new List<Command>();
        private CommandParser Parser = new CommandParser();

        public void ParseAndCallCommand(string commandString)
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

        public void AddCommand(string name, Action commandMethod)
        {
            var comm = new Command(name, commandMethod.Method);
            Commands.Add(comm);
        }

        #region AddCommand Overloads

        public void AddCommand<T>(string name, Action<T> commandMethod)
        {
            var comm = new Command(name, commandMethod.Method);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2>(string name, Action<T1, T2> commandMethod)
        {
            var comm = new Command(name, commandMethod.Method);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3>(string name, Action<T1, T2, T3> commandMethod)
        {
            var comm = new Command(name, commandMethod.Method);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4>(string name, Action<T1, T2, T3, T4> commandMethod)
        {
            var comm = new Command(name, commandMethod.Method);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4, T5>(string name, Action<T1, T2, T3, T4, T5> commandMethod)
        {
            var comm = new Command(name, commandMethod.Method);
            Commands.Add(comm);
        }

        public void AddCommand<T1, T2, T3, T4, T5, T6>(string name, Action<T1, T2, T3, T4, T5, T6> commandMethod)
        {
            var comm = new Command(name, commandMethod.Method);
            Commands.Add(comm);
        }

        #endregion
    }
}