using System;
using System.Collections.Generic;
using Orion.Players.Events;

namespace Orion.Players {
    /// <summary>
    /// Provides a mechanism for managing players.
    /// </summary>
    public interface IPlayerService : IReadOnlyList<IPlayer>, IService {
        /// <summary>
        /// Occurs when a player is being greeted.
        /// </summary>
        event EventHandler<GreetingPlayerEventArgs> GreetingPlayer;

        /// <summary>
        /// Occurs when a player is being updated.
        /// </summary>
        event EventHandler<UpdatingPlayerEventArgs> UpdatingPlayer;

        /// <summary>
        /// Occurs when a player is updated.
        /// </summary>
        event EventHandler<UpdatedPlayerEventArgs> UpdatedPlayer;
    }
}
