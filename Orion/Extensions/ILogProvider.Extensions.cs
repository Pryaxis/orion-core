using Orion.Framework;
using System;

namespace Orion.Extensions
{
    public static class ILogProviderExtensions
    {
        /// <summary>
        /// Logs an exception to the logging provider with an optional message.
        /// </summary>
        public static void LogError(this ILogProvider logProvider, LogOutputFlag flags, Exception exception, string message = null)
        {
            string logMessage = null;
            if (message == null)
                logMessage = $"{exception.ToString()}{Environment.NewLine}";
            else
                logMessage = $"{message}{Environment.NewLine}{exception.ToString()}{Environment.NewLine}";

            logProvider.LogError(flags, logMessage);
        }
    }
}
