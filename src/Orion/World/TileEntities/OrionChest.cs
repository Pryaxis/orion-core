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
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using Orion.Items;
using Orion.Utils;
using Main = Terraria.Main;
using TerrariaChest = Terraria.Chest;
using TerrariaItem = Terraria.Item;

namespace Orion.World.TileEntities {
    internal sealed class OrionChest : AnnotatableObject, IChest {
        public TileEntityType Type => TileEntityType.Chest;
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

        public string Name {
            get => Wrapped?.name ?? string.Empty;
            set {
                if (Wrapped == null) {
                    return;
                }

                Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public IReadOnlyArray<IItem> Items { get; }

        public TerrariaChest? Wrapped { get; }

        public OrionChest(TerrariaChest? terrariaChest) : this(-1, terrariaChest) { }

        public OrionChest(int chestIndex, TerrariaChest? terrariaChest) {
            Index = chestIndex;
            if (terrariaChest is null) {
                Items = EmptyItemArray.Instance;
            } else {
                Items = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                    terrariaChest.item, (_, terrariaItem) => new OrionItem(terrariaItem));
            }

            Wrapped = terrariaChest;
        }
        
        // Not localized because this string is developer-facing.
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => Index >= 0 ? $"#: {Index}" : "chest instance";

        private class EmptyItemArray : IReadOnlyArray<IItem> {
            public static readonly EmptyItemArray Instance = new EmptyItemArray();

            public int Count => 0;
            public IItem this[int index] => throw new IndexOutOfRangeException();

            public IEnumerator<IItem> GetEnumerator() => Enumerable.Empty<IItem>().GetEnumerator();
        }
    }
}
