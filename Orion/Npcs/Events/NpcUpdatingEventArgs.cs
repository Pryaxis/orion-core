using System;
using System.ComponentModel;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcUpdating"/> event.
    /// </summary>
    public sealed class NpcUpdatingEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that is being updated.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcUpdatingEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public NpcUpdatingEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
