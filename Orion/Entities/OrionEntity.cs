using System;
using Microsoft.Xna.Framework;

namespace Orion.Entities {
    /// <summary>
    /// Orion's implementation of <see cref="IEntity"/>.
    /// </summary>
    internal class OrionEntity : IEntity {
        /// <inheritdoc/>
        public int Index => WrappedEntity.whoAmI;

        /// <inheritdoc/>
        public Vector2 Position {
            get => WrappedEntity.position;
            set => WrappedEntity.position = value;
        }

        /// <inheritdoc/>
        public Vector2 Velocity {
            get => WrappedEntity.velocity;
            set => WrappedEntity.velocity = value;
        }

        /// <inheritdoc/>
        public int Width {
            get => WrappedEntity.width;
            set => WrappedEntity.width = value;
        }

        /// <inheritdoc/>
        public int Height {
            get => WrappedEntity.height;
            set => WrappedEntity.height = value;
        }

        /// <inheritdoc/>
        public Terraria.Entity WrappedEntity { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionEntity"/> class wrapping the specified Terraria.Entity
        /// instance.
        /// </summary>
        /// <param name="entity">The Terraria Entity instance to wrap.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public OrionEntity(Terraria.Entity entity) {
            WrappedEntity = entity ?? throw new ArgumentNullException(nameof(entity));
        }
    }
}
