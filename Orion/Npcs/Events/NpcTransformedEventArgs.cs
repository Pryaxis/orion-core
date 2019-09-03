using System;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcTransformed"/> event.
    /// </summary>
    public sealed class NpcTransformedEventArgs : EventArgs {
        /// <summary>
        /// Gets the NPC that was transformed.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcTransformedEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public NpcTransformedEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
