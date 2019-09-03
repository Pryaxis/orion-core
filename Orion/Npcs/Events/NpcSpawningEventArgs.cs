using System.ComponentModel;
using Microsoft.Xna.Framework;
using Orion.Players;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcSpawning"/> event.
    /// </summary>
    public sealed class NpcSpawningEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets the AI bytes.
        /// </summary>
        public float[] AiValues { get; } = new float[4];

        /// <summary>
        /// Gets or sets the NPC's target.
        /// </summary>
        public IPlayer NpcTarget { get; set; }
    }
}
