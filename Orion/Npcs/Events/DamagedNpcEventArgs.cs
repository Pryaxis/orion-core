using System;
using System.ComponentModel;
using Orion.Players;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.DamagedNpc"/> event.
    /// </summary>
    public sealed class DamagedNpcEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that was damaged.
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
        /// Gets the player responsible for damaging the NPC.
        /// </summary>
        public IPlayer PlayerResponsible { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DamagedNpcEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        public DamagedNpcEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
