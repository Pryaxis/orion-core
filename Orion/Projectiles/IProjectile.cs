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
        /// Gets or sets the damage.
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        float Knockback { get; set; }

        /// <summary>
        /// Removes the projectile.
        /// </summary>
        void Remove();
    }
}
