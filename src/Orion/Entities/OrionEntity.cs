// Copyright (c) 2020 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Destructurama.Attributed;
using Microsoft.Xna.Framework;

namespace Orion.Entities {
    // This class is generic so that we only use a single wrapped field instead of two wrapped fields per entity.
    internal abstract class OrionEntity<TTerrariaEntity> : AnnotatableObject, IEntity, IWrapping<TTerrariaEntity>
            where TTerrariaEntity : Terraria.Entity {
        public int Index { get; }

        public bool IsActive {
            get => Wrapped.active;
            set => Wrapped.active = value;
        }

        // Terraria.Entity does not provide this property so we need to declare it.
        public abstract string Name { get; set; }

        public Vector2 Position {
            get => Wrapped.position;
            set => Wrapped.position = value;
        }

        public Vector2 Velocity {
            get => Wrapped.velocity;
            set => Wrapped.velocity = value;
        }

        public Vector2 Size {
            get => Wrapped.Size;
            set => Wrapped.Size = value;
        }

        [NotLogged]
        public TTerrariaEntity Wrapped { get; }

        protected OrionEntity(int entityIndex, TTerrariaEntity terrariaEntity) {
            Debug.Assert(terrariaEntity != null, "Terraria entity should not be null");

            Index = entityIndex;
            Wrapped = terrariaEntity;
        }

        // Not localized because this string is developer-facing.
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => Index >= 0 ? $"{Name} (#: {Index})" : "abstract instance";
    }
}
