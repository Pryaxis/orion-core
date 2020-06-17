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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Destructurama.Attributed;
using Orion.Core.Collections;
using Orion.Core.Entities;
using Orion.Core.Items;
using Orion.Core.World.TileEntities;

namespace Orion.Core.World.Chests
{
    [LogAsScalar]
    internal sealed class OrionChest : AnnotatableObject, IChest, IWrapping<Terraria.Chest>
    {
        public OrionChest(int chestIndex, Terraria.Chest? terrariaChest)
        {
            Index = chestIndex;
            IsActive = terrariaChest != null;
            Wrapped = terrariaChest ?? new Terraria.Chest();
            Items = new ItemArray(Wrapped.item);
        }

        public OrionChest(Terraria.Chest? terrariaChest) : this(-1, terrariaChest) { }

        public string Name
        {
            get => Wrapped.name ?? string.Empty;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IArray<ItemStack> Items { get; }

        public int Index { get; }
        public bool IsActive { get; }

        public int X
        {
            get => Wrapped.x;
            set => Wrapped.x = value;
        }

        public int Y
        {
            get => Wrapped.y;
            set => Wrapped.y = value;
        }

        public Terraria.Chest Wrapped { get; }

        // Not localized because this string is developer-facing.
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => this.IsConcrete() ? $"(index: {Index})" : "<abstract instance>";

        private sealed class ItemArray : IArray<ItemStack>
        {
            private readonly object _lock = new object();
            private readonly Terraria.Item[] _items;

            public ItemArray(Terraria.Item?[] items)
            {
                Debug.Assert(items != null);

                // Initialize the entire array ahead of time so that there are no `null` elements.
                for (var i = 0; i < items.Length; ++i)
                {
                    items[i] ??= new Terraria.Item();
                }
                _items = items!;
            }

            public ItemStack this[int index]
            {
                get
                {
                    if (index < 0 || index >= Count)
                    {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0 to {Count - 1})");
                    }

                    var item = _items[index];

                    // This operation requires a lock since `ItemStack` construction won't be atomic otherwise.
                    lock (_lock)
                    {
                        return new ItemStack((ItemId)item.type, item.stack, (ItemPrefix)item.prefix);
                    }
                }

                set
                {
                    if (index < 0 || index >= Count)
                    {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0 to {Count - 1})");
                    }

                    var item = _items[index];

                    // This operation requires a lock since `ItemStack` assignment won't be atomic otherwise.
                    lock (_lock)
                    {
                        item.type = (int)value.Id;
                        item.stack = value.StackSize;
                        item.prefix = (byte)value.Prefix;
                    }
                }
            }

            public int Count => _items.Length;
        }
    }
}
