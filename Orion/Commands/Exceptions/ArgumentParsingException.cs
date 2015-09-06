using System;

namespace Orion.Commands
{
    public class ArgumentParsingException : Exception
    {
        public ArgumentParsingException() { }

        public ArgumentParsingException(string message, Exception innerException) : base(message, innerException) { }

        public ArgumentParsingException(string message) : base(message) { }
    }
}