using Orion.Entities;

namespace Orion.Npcs {
    /// <summary>
    /// Provides a wrapper arround a Terraria.NPC instance.
    /// </summary>
    public interface INpc : IEntity {
        /// <summary>
        /// Gets or sets the NPC's type.
        /// </summary>
        NpcType Type { get; set; }

        /// <summary>
        /// Gets or sets the NPC's HP.
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// Gets or sets the NPC's max HP.
        /// </summary>
        int MaxHp { get; set; }

        /// <summary>
        /// Damages the NPC with the specified damage, knockback, hit direction, and criticality.
        /// </summary>
        /// <param name="damage">The damage.</param>
        /// <param name="knockback">The knockback.</param>
        /// <param name="hitDirection">The hit direction.</param>
        /// <param name="isCritical">A value indicating whether the hit should be critical.</param>
        void Damage(int damage, float knockback, int hitDirection, bool isCritical = false);

        /// <summary>
        /// Kills the NPC.
        /// </summary>
        void Kill();

        /// <summary>
        /// Transforms the NPC to a new type.
        /// </summary>
        /// <param name="newType">The new type.</param>
        void Transform(NpcType newType);
    }
}
