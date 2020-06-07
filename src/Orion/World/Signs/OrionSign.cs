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
using Orion.Entities;

namespace Orion.World.Signs {
    internal sealed class OrionSign : AnnotatableObject, ISign, IWrapping<Terraria.Sign?> {
        public OrionSign(int signIndex, Terraria.Sign? terrariaSign) {
            Index = signIndex;
            Wrapped = terrariaSign;
        }
        public OrionSign(Terraria.Sign? terrariaSign) : this(-1, terrariaSign) { }

        public string Text {
            get => Wrapped?.text ?? string.Empty;
            set {
                if (value is null) {
                    throw new ArgumentNullException(nameof(value));
                }

                if (Wrapped is null) {
                    return;
                }

                Wrapped.text = value;
            }
        }

        public int Index { get; }
        public bool IsActive => Wrapped != null;

        public int X {
            get => Wrapped?.x ?? 0;
            set {
                if (Wrapped is null) {
                    return;
                }

                Wrapped.x = value;
            }
        }

        public int Y {
            get => Wrapped?.y ?? 0;
            set {
                if (Wrapped is null) {
                    return;
                }

                Wrapped.y = value;
            }
        }

        public Terraria.Sign? Wrapped { get; }
    }
}
