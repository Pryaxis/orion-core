using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
    /// <summary>
    /// Defines an Orion hook provider.
    /// </summary>
    public interface IHookProvider
    {
        /// <summary>
        /// Occurs each time the game loop starts
        /// </summary>
        event Events.OrionEventHandler GameUpdate;

        /// <summary>
        /// Occurs after the game loop finishes
        /// </summary>
        event Events.OrionEventHandler GamePostUpdate;
    }
}
