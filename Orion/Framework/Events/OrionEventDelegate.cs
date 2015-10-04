using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Events
{
    /// <summary>
    /// Delegate that describes all .NET events in Orion
    /// </summary>
    /// <param name="orion">A reference to the Orion instance which initiated the event</param>
    /// <param name="e">A reference to the Orion event arguments</param>
    public delegate void OrionEventHandler(Orion orion, OrionEventArgs e);

    /// <summary>
    /// Delegate that generically describes all .NET events in Orion
    /// </summary>
    /// <param name="orion">A reference to the Orion instance which initiated the event</param>
    /// <param name="e">A reference to the Orion event arguments</param>
    public delegate void OrionEventHandler<TArgs>(Orion orion, TArgs e) where TArgs: OrionEventArgs;

    /// <summary>
    /// EventArgs that describes all Orion events.  This EventArgs has no properties.
    /// </summary>
    public class OrionEventArgs : EventArgs
    {
        public bool Cancelled { get; set; }
    }
}