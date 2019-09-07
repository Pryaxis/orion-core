using System;

namespace Orion.World.Events {
    /// <summary>
    /// Provides data for the <see cref="IWorldService.SavedWorld"/> event.
    /// </summary>
    public sealed class SavedWorldEventArgs : EventArgs {
        /// <summary>
        /// Gets a value indicating whether to reset the time.
        /// </summary>
        public bool ShouldResetTime { get; internal set; }
    }
}
