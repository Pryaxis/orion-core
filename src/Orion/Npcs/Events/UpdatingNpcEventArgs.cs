using System;
using System.ComponentModel;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.UpdatingNpc"/> event.
    /// </summary>
    public sealed class UpdatingNpcEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that is being updated.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingNpcEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public UpdatingNpcEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
