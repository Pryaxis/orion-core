using System.Diagnostics;
using Orion.Entities;

namespace Orion.Npcs {
    /// <summary>
    /// Orion's implementation of <see cref="INpc"/>.
    /// </summary>
    internal sealed class OrionNpc : OrionEntity, INpc {
        public NpcType Type {
            get => (NpcType)WrappedNpc.netID;
            set {
                WrappedNpc.type = Terraria.ID.NPCID.FromNetId((int)value);
                WrappedNpc.netID = (int)value;
            }
        }

        public int Hp {
            get => WrappedNpc.life;
            set => WrappedNpc.life = value;
        }

        public int MaxHp {
            get => WrappedNpc.lifeMax;
            set => WrappedNpc.lifeMax = value;
        }

        internal Terraria.NPC WrappedNpc { get; }

        public OrionNpc(Terraria.NPC terrariaNpc) : base(terrariaNpc) {
            Debug.Assert(terrariaNpc != null, $"{nameof(terrariaNpc)} should not be null.");

            WrappedNpc = terrariaNpc;
        }

        public void Damage(int damage, float knockback, int hitDirection, bool isCritical = false) {
            WrappedNpc.StrikeNPC(damage, knockback, hitDirection, isCritical);
        }

        public void Kill() {
            Hp = 0;
            WrappedNpc.checkDead();
            IsActive = false;
        }

        public void Transform(NpcType newType) => WrappedNpc.Transform((int)newType);
    }
}
