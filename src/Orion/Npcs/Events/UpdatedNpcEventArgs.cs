using System;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.UpdatedNpc"/> event.
    /// </summary>
    public sealed class UpdatedNpcEventArgs : EventArgs {
        /// <summary>
        /// Gets the NPC that was updated.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatedNpcEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public UpdatedNpcEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
