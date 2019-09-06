using System;
using Orion.Entities;

namespace Orion.Projectiles {
    internal sealed class OrionProjectile : OrionEntity<Terraria.Projectile>, IProjectile {
        private string _nameOverride;

        public override string Name {
            get => _nameOverride ?? Wrapped.Name;
            set => _nameOverride = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ProjectileType Type => (ProjectileType)Wrapped.type;

        public int Damage {
            get => Wrapped.damage;
            set => Wrapped.damage = value;
        }

        public float Knockback {
            get => Wrapped.knockBack;
            set => Wrapped.knockBack = value;
        }

        public OrionProjectile(Terraria.Projectile terrariaProjectile) : base(terrariaProjectile) { }

        public void ApplyType(ProjectileType type) => Wrapped.SetDefaults((int)type);
        public void Remove() => Wrapped.Kill();
    }
}
