using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Provides a wrapper around a Terraria.Player instance.
    /// </summary>
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets or sets the player's HP.
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// Gets or sets the player's max HP.
        /// </summary>
        int MaxHp { get; set; }

        /// <summary>
        /// Gets or sets the player's MP.
        /// </summary>
        int Mp { get; set; }

        /// <summary>
        /// Gets or sets the player's max MP.
        /// </summary>
        int MaxMp { get; set; }
    }
}
