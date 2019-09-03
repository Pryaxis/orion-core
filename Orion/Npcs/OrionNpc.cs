using System.Diagnostics;
using Orion.Entities;

namespace Orion.Npcs {
    /// <summary>
    /// Orion's implementation of <see cref="INpc"/>.
    /// </summary>
    internal sealed class OrionNpc : OrionEntity, INpc {
        public NpcType Type {
            get => (NpcType)WrappedNpc.type;
            set => WrappedNpc.type = (int)value;
        }

        public Terraria.NPC WrappedNpc { get; }

        public OrionNpc(Terraria.NPC terrariaNpc) : base(terrariaNpc) {
            Debug.Assert(terrariaNpc != null, $"{nameof(terrariaNpc)} should not be null.");

            WrappedNpc = terrariaNpc;
        }
    }
}
