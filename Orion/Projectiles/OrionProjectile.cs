using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionProjectile"/> class wrapping the specified Terraria
        /// Projectile instance.
        /// </summary>
        /// <param name="terrariaProjectile">The projectile.</param>
        /// <exception cref="ArgumentNullException"><paramref name="terrariaProjectile"/> is <c>null</c>.</exception>
        public OrionProjectile(Terraria.Projectile terrariaProjectile) : base(terrariaProjectile) {
            WrappedProjectile = terrariaProjectile ?? throw new ArgumentNullException();
        }
    }
}
