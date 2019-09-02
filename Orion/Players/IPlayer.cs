using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Provides a wrapper around a Terraria.Player instance.
    /// </summary>
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets the wrapped Terraria Player instance.
        /// </summary>
        Terraria.Player WrappedPlayer { get; }
    }
}
