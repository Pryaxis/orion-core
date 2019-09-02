using System;
using System.ComponentModel;

namespace Orion.Players.Events {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerJoining"/> event.
    /// </summary>
    public sealed class PlayerJoiningEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the player that is joining.
        /// </summary>
        public IPlayer Player { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerJoiningEventArgs"/> class with the specified player.
        /// </summary>
        /// <param name="player">The player that is joining the game.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
        public PlayerJoiningEventArgs(IPlayer player) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }
    }
}
