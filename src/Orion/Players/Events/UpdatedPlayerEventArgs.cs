using System;

namespace Orion.Players.Events {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.UpdatedPlayer"/> event.
    /// </summary>
    public sealed class UpdatedPlayerEventArgs : EventArgs {
        /// <summary>
        /// Gets the player that was updated.
        /// </summary>
        public IPlayer Player { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatedPlayerEventArgs"/> class with the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
        public UpdatedPlayerEventArgs(IPlayer player) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }
    }
}
