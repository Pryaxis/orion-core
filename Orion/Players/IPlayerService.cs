using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Players.Events;

namespace Orion.Players {
    /// <summary>
    /// Provides a mechanism for managing players. All implementations must be thread-safe.
    /// </summary>
    public interface IPlayerService : IReadOnlyList<IPlayer>, IService {
        /// <summary>
        /// Occurs when a player is joining the game.
        /// </summary>
        event EventHandler<PlayerJoiningEventArgs> PlayerJoining;

        /// <summary>
        /// Occurs when a player has joined the game.
        /// </summary>
        event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

        /// <summary>
        /// Occurs when a player is being updated.
        /// </summary>
        event EventHandler<PlayerUpdatingEventArgs> PlayerUpdating;

        /// <summary>
        /// Occurs when a player is updated.
        /// </summary>
        event EventHandler<PlayerUpdatedEventArgs> PlayerUpdated;

        /// <summary>
        /// Occurs when a player has quit the game.
        /// </summary>
        event EventHandler<PlayerQuitEventArgs> PlayerQuit;
    }
}
