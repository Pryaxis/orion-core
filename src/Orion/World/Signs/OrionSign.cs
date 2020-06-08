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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Destructurama.Attributed;
using Orion.Entities;
using Orion.World.TileEntities;

namespace Orion.World.Signs {
    [LogAsScalar]
    internal sealed class OrionSign : AnnotatableObject, ISign, IWrapping<Terraria.Sign> {
        public OrionSign(int signIndex, Terraria.Sign? terrariaSign) {
            Index = signIndex;
            IsActive = terrariaSign != null;
            Wrapped = terrariaSign ?? new Terraria.Sign();
        }

        public OrionSign(Terraria.Sign? terrariaSign) : this(-1, terrariaSign) { }

        public string Text {
            get => Wrapped.text ?? string.Empty;
            set => Wrapped.text = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Index { get; }

        public bool IsActive { get; }

        public int X {
            get => Wrapped.x;
            set => Wrapped.x = value;
        }

        public int Y {
            get => Wrapped.y;
            set => Wrapped.y = value;
        }

        public Terraria.Sign Wrapped { get; }

        // Not localized because this string is developer-facing.
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => this.IsConcrete() ? $"(index: {Index})" : "<abstract instance>";
    }
}
