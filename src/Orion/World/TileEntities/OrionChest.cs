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
using Orion.Items;
using Orion.Utils;
using Main = Terraria.Main;
using TerrariaChest = Terraria.Chest;
using TerrariaItem = Terraria.Item;

namespace Orion.World.TileEntities {
    internal sealed class OrionChest : AnnotatableObject, IChest {
        public TileEntityType Type => TileEntityType.Chest;

        public int Index { get; }

        public int X {
            get => Wrapped.x;
            set => Wrapped.x = value;
        }

        public int Y {
            get => Wrapped.y;
            set => Wrapped.y = value;
        }

        public string Name {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IReadOnlyArray<IItem> Items { get; }

        public TerrariaChest Wrapped { get; }

        public OrionChest(int chestIndex, TerrariaChest terrariaChest) {
            Debug.Assert(chestIndex >= 0 && chestIndex < Main.maxChests,
                         "chestIndex >= 0 && chestIndex < Main.maxChests");
            Debug.Assert(terrariaChest != null, "terrariaChest != null");

            Index = chestIndex;
            Wrapped = terrariaChest;
            Items = new WrappedReadOnlyArray<OrionItem, TerrariaItem>(
                Wrapped.item, (_, terrariaItem) => new OrionItem(terrariaItem));
        }
    }
}
