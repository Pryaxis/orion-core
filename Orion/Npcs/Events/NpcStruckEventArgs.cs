using System;
using System.ComponentModel;
using Orion.Players;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcStruck"/> event.
    /// </summary>
    public sealed class NpcStruckEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that was struck.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public int Damage { get; internal set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float Knockback { get; internal set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        public int HitDirection { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsCriticalHit { get; internal set; }

        /// <summary>
        /// Gets the strike player.
        /// </summary>
        public IPlayer StrikePlayer { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcStruckEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        public NpcStruckEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
