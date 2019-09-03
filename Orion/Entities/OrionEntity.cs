using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Orion.Entities {
    /// <summary>
    /// Orion's implementation of <see cref="IEntity"/>.
    /// </summary>
    internal class OrionEntity : IEntity {
        private readonly Terraria.Entity _wrappedEntity;

        public int Index => _wrappedEntity.whoAmI;

        public Vector2 Position {
            get => _wrappedEntity.position;
            set => _wrappedEntity.position = value;
        }

        public Vector2 Velocity {
            get => _wrappedEntity.velocity;
            set => _wrappedEntity.velocity = value;
        }

        public Vector2 Size {
            get => _wrappedEntity.Size;
            set => _wrappedEntity.Size = value;
        }

        public OrionEntity(Terraria.Entity entity) {
            Debug.Assert(entity != null, $"{nameof(entity)} should not be null.");

            _wrappedEntity = entity;
        }
    }
}
