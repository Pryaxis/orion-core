using System;
using Microsoft.Xna.Framework;

namespace Orion.Entities {
    /// <summary>
    /// Orion's implementation of <see cref="IEntity"/>.
    /// </summary>
    internal class OrionEntity : IEntity {
        private readonly Terraria.Entity _wrappedEntity;

        /// <inheritdoc/>
        public int Index => _wrappedEntity.whoAmI;

        /// <inheritdoc/>
        public Vector2 Position {
            get => _wrappedEntity.position;
            set => _wrappedEntity.position = value;
        }

        /// <inheritdoc/>
        public Vector2 Velocity {
            get => _wrappedEntity.velocity;
            set => _wrappedEntity.velocity = value;
        }

        /// <inheritdoc/>
        public Vector2 Size {
            get => _wrappedEntity.Size;
            set => _wrappedEntity.Size = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionEntity"/> class wrapping the specified Terraria.Entity
        /// instance.
        /// </summary>
        /// <param name="entity">The Terraria Entity instance to wrap.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public OrionEntity(Terraria.Entity entity) {
            _wrappedEntity = entity ?? throw new ArgumentNullException(nameof(entity));
        }
    }
}
