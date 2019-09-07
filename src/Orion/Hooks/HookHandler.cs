using System;

namespace Orion.Hooks {
    /// <summary>
    /// Represents a hook handler.
    /// </summary>
    /// <typeparam name="TArgs">The type of event arguments.</typeparam>
    /// <param name="sender">The sender. This is usually the service instance which initiated the event.</param>
    /// <param name="args">The event arguments.</param>
    public delegate void HookHandler<in TArgs>(object sender, TArgs args) where TArgs : EventArgs;
}
