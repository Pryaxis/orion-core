namespace Orion.Entities {
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Provides a wrapper around a <see cref="Terraria.Entity"/>.
    /// </summary>
    public interface IEntity {
        /// <summary>
        /// Gets or sets the entity's height in pixels.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets the entity's position in the world.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the entity's velocity in the world.
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the entity's width in pixels.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets the wrapped <see cref="Terraria.Entity"/> instance.
        /// </summary>
        Terraria.Entity WrappedEntity { get; }
    }
}
