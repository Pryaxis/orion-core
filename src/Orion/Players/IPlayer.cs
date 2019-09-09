using Orion.Entities;
using Orion.Utils;

namespace Orion.Players {
    /// <summary>
    /// Provides a wrapper around a Terraria.Player instance.
    /// </summary>
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        PlayerDifficulty Difficulty { get; set; }

        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum health.
        /// </summary>
        int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the player's mana.
        /// </summary>
        int Mana { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum mana.
        /// </summary>
        int MaxMana { get; set; }

        /// <summary>
        /// Gets or sets the player's team.
        /// </summary>
        PlayerTeam Team { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in PvP.
        /// </summary>
        bool IsInPvp { get; set; }

        /// <summary>
        /// Gets the player's buffs.
        /// </summary>
        IArray<Buff> Buffs { get; }

        /// <summary>
        /// Adds the given buff to the player.
        /// </summary>
        /// <param name="buff">The buff.</param>
        void AddBuff(Buff buff);
    }
}
