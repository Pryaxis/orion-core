using System.ComponentModel;

namespace Orion.World.Events {
    /// <summary>
    /// Provides data for the <see cref="IWorldService.SavingWorld"/> event.
    /// </summary>
    public sealed class SavingWorldEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets or sets a value indicating whether to reset the time.
        /// </summary>
        public bool ShouldResetTime { get; set; }
    }
}
