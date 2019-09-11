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

using System;
using System.Diagnostics;
using Orion.Utils;
using Terraria;

namespace Orion.World.TileEntities {
    internal sealed class OrionSign : AnnotatableObject, ISign {
        public int Index { get; }

        public int X {
            get => Wrapped.x;
            set => Wrapped.x = value;
        }

        public int Y {
            get => Wrapped.y;
            set => Wrapped.y = value;
        }

        public string Text {
            get => Wrapped.text;
            set => Wrapped.text = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal Sign Wrapped { get; }

        public OrionSign(int signIndex, Sign terrariaSign) {
            Debug.Assert(signIndex >= 0 && signIndex < Sign.maxSigns,
                         $"{nameof(signIndex)} should be a valid index.");
            Debug.Assert(terrariaSign != null, $"{nameof(terrariaSign)} should not be null.");

            Index = signIndex;
            Wrapped = terrariaSign;
        }
    }
}
