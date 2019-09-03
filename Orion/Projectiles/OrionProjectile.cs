using System.Diagnostics;
using Orion.Entities;

namespace Orion.Projectiles {
    /// <summary>
    /// Orion's implementation of <see cref="IProjectile"/>.
    /// </summary>
    internal sealed class OrionProjectile : OrionEntity, IProjectile {
        public ProjectileType Type {
            get => (ProjectileType)WrappedProjectile.type;
            set => WrappedProjectile.type = (int)value;
        }

        public Terraria.Projectile WrappedProjectile { get; }

        public OrionProjectile(Terraria.Projectile terrariaProjectile) : base(terrariaProjectile) {
            Debug.Assert(terrariaProjectile != null, $"{nameof(terrariaProjectile)} should not be null.");

            WrappedProjectile = terrariaProjectile;
        }
    }
}
