using Microsoft.Xna.Framework;

namespace Orion.Entities {
    /// <summary>
    /// Provides a wrapper around a Terraria.Entity instance.
    /// </summary>
    public interface IEntity {
        /// <summary>
        /// Gets the entity's index.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets or sets the entity's position in the world.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the entity's velocity in the world.
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the entity's size in the world.
        /// </summary>
        Vector2 Size { get; set; }
    }
}
