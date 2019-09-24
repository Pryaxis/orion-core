// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Utils;

namespace Orion.Entities {
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

        [NotNull] internal TTerrariaEntity Wrapped { get; }

        protected OrionEntity([NotNull] TTerrariaEntity terrariaEntity) {
            Wrapped = terrariaEntity;
        }
    }
}
