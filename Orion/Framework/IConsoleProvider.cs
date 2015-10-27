using Orion.Framework.Events;
using OTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
    /// <summary>
    /// Describes an Orion console provider, which manages the .NET console/terminal
    /// window and input from the user.
    /// </summary>
    public interface IConsoleProvider
    {
        /// <summary>
        /// Gets or sets the player in which the console is running under. Null is 
        /// the server.
        /// </summary>
        BasePlayer ActivePlayer { get; set; }

        /// <summary>
        /// Occurs when someone types a line in the console.  Could be chat, or a command.
        /// </summary>
        event EventHandler<ConsoleLineEventArgs> ConsoleLine;
    }
}
