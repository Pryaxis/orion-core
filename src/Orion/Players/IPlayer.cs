using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Provides a wrapper around a Terraria.Player instance.
    /// </summary>
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum health.
        /// </summary>
        int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the player's MP.
        /// </summary>
        int Mp { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum MP.
        /// </summary>
        int MaxMp { get; set; }
    }
}
