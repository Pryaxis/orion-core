using System;
using System.Diagnostics.CodeAnalysis;
using Orion.Entities;

namespace Orion.Npcs {
    internal sealed class OrionNpc : OrionEntity<Terraria.NPC>, INpc {
        public override string Name {
            get => Wrapped.GivenOrTypeName;
            set => Wrapped._givenName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public NpcType Type => (NpcType)Wrapped.netID;

        public int Health {
            get => Wrapped.life;
            set => Wrapped.life = value;
        }

        public int MaxHealth {
            get => Wrapped.lifeMax;
            set => Wrapped.lifeMax = value;
        }

        public OrionNpc(Terraria.NPC terrariaNpc) : base(terrariaNpc) { }

        public void ApplyType(NpcType type) {
            Wrapped.SetDefaults((int)type);
        }

        public void Damage(int damage, float knockback, int hitDirection, bool isCritical = false) {
            Wrapped.StrikeNPC(damage, knockback, hitDirection, isCritical);
        }

        public void Kill() {
            Health = 0;
            Wrapped.checkDead();
            IsActive = false;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type} @ ({Position})";

        public void Transform(NpcType newType) {
            Wrapped.Transform((int)newType);
        }
    }
}
