using System;
using System.ComponentModel;
using Orion.Players;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcStriking"/> event.
    /// </summary>
    public sealed class NpcStrikingEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that is being struck.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        public int HitDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsCriticalHit { get; set; }

        /// <summary>
        /// Gets the strike player.
        /// </summary>
        public IPlayer StrikePlayer { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcStrikingEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        public NpcStrikingEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
