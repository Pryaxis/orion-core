// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Microsoft.Xna.Framework;
using Orion.Utils;

namespace Orion.Entities {
    // OrionEntity is generic so that we only use a single wrapped field instead of two wrapped fields per entity.
    internal abstract class OrionEntity<TTerrariaEntity> : AnnotatableObject, IEntity
        where TTerrariaEntity : Terraria.Entity {
        public int Index => Wrapped.whoAmI;

        public bool IsActive {
            get => Wrapped.active;
            set => Wrapped.active = value;
        }

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

        // internal for testing purposes.
        internal TTerrariaEntity Wrapped { get; }

        protected OrionEntity(TTerrariaEntity terrariaEntity) {
            Debug.Assert(terrariaEntity != null, "terrariaEntity != null");

            Wrapped = terrariaEntity;
        }
    }
}
