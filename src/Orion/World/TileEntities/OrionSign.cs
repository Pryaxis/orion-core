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

using System;
using System.Diagnostics;
using Orion.Utils;
using TerrariaSign = Terraria.Sign;

namespace Orion.World.TileEntities {
    internal sealed class OrionSign : AnnotatableObject, ISign {
        public TileEntityType Type => TileEntityType.Sign;
        public int Index { get; }
        public bool IsActive => Wrapped != null;

        public int X {
            get => Wrapped?.x ?? 0;
            set {
                if (Wrapped == null) {
                    return;
                }

                Wrapped.x = value;
            }
        }

        public int Y {
            get => Wrapped?.y ?? 0;
            set {
                if (Wrapped == null) {
                    return;
                }

                Wrapped.y = value;
            }
        }

        public string Text {
            get => Wrapped?.text ?? string.Empty;
            set {
                if (Wrapped == null) {
                    return;
                }

                Wrapped.text = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public TerrariaSign? Wrapped { get; }

        public OrionSign(int signIndex, TerrariaSign? terrariaSign) {
            Debug.Assert(signIndex >= 0 && signIndex < TerrariaSign.maxSigns, "sign index should be valid");

            Index = signIndex;
            Wrapped = terrariaSign;
        }
    }
}
