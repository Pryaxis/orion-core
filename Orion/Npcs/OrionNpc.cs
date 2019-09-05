using System;
using System.Diagnostics;
using Orion.Entities;

namespace Orion.Npcs {
    /// <summary>
    /// Orion's implementation of <see cref="INpc"/>.
    /// </summary>
    internal sealed class OrionNpc : OrionEntity<Terraria.NPC>, INpc {
        public override string Name {
            get => Wrapped.GivenOrTypeName;
            set => Wrapped._givenName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public NpcType Type {
            get => (NpcType)Wrapped.netID;
            set {
                /*
                 * To properly handle the type, we need to set both type and netID!
                 */
                Wrapped.type = Terraria.ID.NPCID.FromNetId((int)value);
                Wrapped.netID = (int)value;
            }
        }

        public int Hp {
            get => Wrapped.life;
            set => Wrapped.life = value;
        }

        public int MaxHp {
            get => Wrapped.lifeMax;
            set => Wrapped.lifeMax = value;
        }

        public OrionNpc(Terraria.NPC terrariaNpc) : base(terrariaNpc) { }

        public void Damage(int damage, float knockback, int hitDirection, bool isCritical = false) =>
            Wrapped.StrikeNPC(damage, knockback, hitDirection, isCritical);

        public void Kill() {
            Hp = 0;
            Wrapped.checkDead();
            IsActive = false;
        }

        public void Transform(NpcType newType) => Wrapped.Transform((int)newType);
    }
}
