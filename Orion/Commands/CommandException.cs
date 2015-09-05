using System;

namespace Orion.Commands
{
    public class CommandException : Exception
    {
        public CommandException(string message) : base(message) { }
    }
}