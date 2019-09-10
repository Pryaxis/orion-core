using Orion.Entities;

namespace Orion.Projectiles {
    /// <summary>
    /// Provides a wrapper around a Terraria.Projectile.
    /// </summary>
    public interface IProjectile : IEntity {
        /// <summary>
        /// Gets the projectile type.
        /// </summary>
        ProjectileType Type { get; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        float Knockback { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the projectile is hostile.
        /// </summary>
        bool IsHostile { get; set; }

        /// <summary>
        /// Applies the given type to the projectile.
        /// </summary>
        /// <param name="type">The type.</param>
        void ApplyType(ProjectileType type);

        /// <summary>
        /// Removes the projectile.
        /// </summary>
        void Remove();
    }
}
