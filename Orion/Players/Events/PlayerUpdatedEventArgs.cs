using System;

namespace Orion.Players.Events {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerUpdated"/> event.
    /// </summary>
    public sealed class PlayerUpdatedEventArgs : EventArgs {
        /// <summary>
        /// Gets the player that was updated.
        /// </summary>
        public IPlayer Player { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUpdatedEventArgs"/> class with the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
        public PlayerUpdatedEventArgs(IPlayer player) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }
    }
}
