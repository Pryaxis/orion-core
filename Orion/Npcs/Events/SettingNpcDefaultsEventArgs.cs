using System;
using System.ComponentModel;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.SettingNpcDefaults"/> event.
    /// </summary>
    public sealed class SettingNpcDefaultsEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that is having its defaults set.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets or sets the <see cref="NpcType"/> that the NPC is having its defaults set to.
        /// </summary>
        public NpcType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingNpcDefaultsEventArgs"/> class with the specified NPC
        /// and NPC type.
        /// </summary>
        /// <param name="npc">The NPC that is having its defaults set.</param>
        /// <param name="type">The NPC type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public SettingNpcDefaultsEventArgs(INpc npc, NpcType type) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
            Type = type;
        }
    }
}
