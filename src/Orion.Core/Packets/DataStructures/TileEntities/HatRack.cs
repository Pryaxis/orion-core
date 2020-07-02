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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Items;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    /// <summary>
    /// Represents a serializable hat rack.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public sealed class HatRack : SerializableTileEntity
    {
        [FieldOffset(0)] private ItemStack _items;  // Used to obtain an interior reference.
        [FieldOffset(16)] private ItemStack _dyes;  // Used to obtain an interior reference.

        /// <inheritdoc/>
        public override TileEntityId Id => TileEntityId.HatRack;

        /// <summary>
        /// Gets the hat rack's items.
        /// </summary>
        /// <value>The hat rack's items.</value>
        public Span<ItemStack> Items => MemoryMarshal.CreateSpan(ref _items, 2);

        /// <summary>
        /// Gets the hat rack's dyes.
        /// </summary>
        /// <value>The hat rack's dyes.</value>
        public Span<ItemStack> Dyes => MemoryMarshal.CreateSpan(ref _dyes, 2);

        /// <inheritdoc/>
        protected override int ReadBody(Span<byte> span)
        {
            ref var flags = ref Unsafe.As<byte, Flags8>(ref span.At(0));
            var length = 1;

            for (var i = 0; i < 2; ++i)
            {
                if (flags[i])
                {
                    length += span[length..].Read(ref Items.At(i).AsByte(), 5);
                }
            }

            for (var i = 0; i < 2; ++i)
            {
                if (flags[i + 2])
                {
                    length += span[length..].Read(ref Dyes.At(i).AsByte(), 5);
                }
            }

            return length;
        }

        /// <inheritdoc/>
        protected override int WriteBody(Span<byte> span)
        {
            ref var flags = ref Unsafe.As<byte, Flags8>(ref span.At(0));
            flags = default;
            var length = 1;

            for (var i = 0; i < 2; ++i)
            {
                ref var item = ref Items.At(0);
                if (!item.IsEmpty)
                {
                    length += span[length..].Write(ref item.AsByte(), 5);
                    flags[i] = true;
                }
            }

            for (var i = 0; i < 2; ++i)
            {
                ref var dye = ref Dyes.At(0);
                if (!dye.IsEmpty)
                {
                    length += span[length..].Write(ref dye.AsByte(), 5);
                    flags[i + 2] = true;
                }
            }

            return length;
        }
    }
}
