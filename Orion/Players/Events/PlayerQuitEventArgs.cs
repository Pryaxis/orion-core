using System;

namespace Orion.Players.Events {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerQuit"/> event.
    /// </summary>
    public sealed class PlayerQuitEventArgs : EventArgs {
        /// <summary>
        /// Gets the player that quit.
        /// </summary>
        public IPlayer Player { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerQuitEventArgs"/> class with the specified player.
        /// </summary>
        /// <param name="player">The player that quit the game.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
        public PlayerQuitEventArgs(IPlayer player) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }
    }
}
