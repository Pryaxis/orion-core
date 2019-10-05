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
using Orion.Utils;
using TerrariaItem = Terraria.Item;

namespace Orion.Items {
    internal sealed class OrionItemStats : IItemStats, IWrapping<TerrariaItem> {
        public int MaxStackSize => Wrapped.maxStack;
        public ItemRarity Rarity => (ItemRarity)Wrapped.rare;

        public TerrariaItem Wrapped { get; }

        public OrionItemStats(TerrariaItem terrariaItem) {
            Debug.Assert(terrariaItem != null, "Terraria item should not be null");

            Wrapped = terrariaItem;
        }
    }
}
