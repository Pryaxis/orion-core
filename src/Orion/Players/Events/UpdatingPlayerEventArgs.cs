using System;
using System.ComponentModel;

namespace Orion.Players.Events {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.UpdatingPlayer"/> event.
    /// </summary>
    public sealed class UpdatingPlayerEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the player that is being updated.
        /// </summary>
        public IPlayer Player { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingPlayerEventArgs"/> class with the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is null.</exception>
        public UpdatingPlayerEventArgs(IPlayer player) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }
    }
}
