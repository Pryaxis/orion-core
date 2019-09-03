using System;
using Orion.Entities;

namespace Orion.Npcs {
    /// <summary>
    /// Orion's implementation of <see cref="INpc"/>.
    /// </summary>
    internal sealed class OrionNpc : OrionEntity, INpc {
        /// <inheritdoc />
        public NpcType Type {
            get => (NpcType)WrappedNpc.type;
            set => WrappedNpc.type = (int)value;
        }

        /// <inheritdoc />
        public Terraria.NPC WrappedNpc { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionNpc"/> class wrapping the specified Terraria NPC
        /// instance.
        /// </summary>
        /// <param name="terrariaNpc">The projectile.</param>
        /// <exception cref="ArgumentNullException"><paramref name="terrariaNpc"/> is <c>null</c>.</exception>
        public OrionNpc(Terraria.NPC terrariaNpc) : base(terrariaNpc) {
            WrappedNpc = terrariaNpc ?? throw new ArgumentNullException();
        }
    }
}
