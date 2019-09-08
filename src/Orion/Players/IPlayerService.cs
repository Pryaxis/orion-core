using System.Collections.Generic;
using Orion.Hooks;
using Orion.Players.Events;

namespace Orion.Players {
    /// <summary>
    /// Provides a mechanism for managing players.
    /// </summary>
    public interface IPlayerService : IReadOnlyList<IPlayer>, IService {
        /// <summary>
        /// Occurs when a player is being greeted.
        /// </summary>
        HookHandlerCollection<GreetingPlayerEventArgs> GreetingPlayer { get; set; }

        /// <summary>
        /// Occurs when a player is being updated.
        /// </summary>
        HookHandlerCollection<UpdatingPlayerEventArgs> UpdatingPlayer { get; set; }

        /// <summary>
        /// Occurs when a player is updated.
        /// </summary>
        HookHandlerCollection<UpdatedPlayerEventArgs> UpdatedPlayer { get; set; }
    }
}
