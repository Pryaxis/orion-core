using Orion.Entities;

namespace Orion.Projectiles {
    /// <summary>
    /// Provides a wrapper around a Terraria.Projectile.
    /// </summary>
    public interface IProjectile : IEntity {
        /// <summary>
        /// Gets or sets the projectile type.
        /// </summary>
        ProjectileType Type { get; set; }

        /// <summary>
        /// Gets the wrapped Terraria Projectile instance.
        /// </summary>
        new Terraria.Projectile WrappedEntity { get; }
    }
}
