using System;

namespace Orion.Players.Events {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerJoined"/> event.
    /// </summary>
    public sealed class PlayerJoinedEventArgs : EventArgs {
        /// <summary>
        /// Gets the player that joined.
        /// </summary>
        public IPlayer Player { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class with the specified player.
        /// </summary>
        /// <param name="player">The player that joined.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
        public PlayerJoinedEventArgs(IPlayer player) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }
    }
}
