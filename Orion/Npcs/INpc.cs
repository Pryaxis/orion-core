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
        /// Gets the wrapped Terraria NPC instance.
        /// </summary>
        Terraria.NPC WrappedNpc { get; }
    }
}
