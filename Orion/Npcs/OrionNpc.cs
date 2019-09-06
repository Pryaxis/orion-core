using System;
using Orion.Entities;

namespace Orion.Npcs {
    internal sealed class OrionNpc : OrionEntity<Terraria.NPC>, INpc {
        public override string Name {
            get => Wrapped.GivenOrTypeName;
            set => Wrapped._givenName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public NpcType Type => (NpcType)Wrapped.netID;

        public int Hp {
            get => Wrapped.life;
            set => Wrapped.life = value;
        }

        public int MaxHp {
            get => Wrapped.lifeMax;
            set => Wrapped.lifeMax = value;
        }

        public OrionNpc(Terraria.NPC terrariaNpc) : base(terrariaNpc) { }

        public void ApplyType(NpcType type) => Wrapped.SetDefaults((int)type);

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
