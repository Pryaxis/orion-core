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
using Orion.Core.World.TileEntities;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    /// <summary>
    /// Represents a serializable hat rack.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public sealed class HatRack : SerializableTileEntity
    {
        [FieldOffset(0)] private ItemStack _items;
        [FieldOffset(16)] private ItemStack _dyes;

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
            var flags = Unsafe.ReadUnaligned<ByteFlags>(ref span[0]);
            var length = 1;

            if (flags[0])
            {
                length += span[length..].Read(ref Unsafe.As<ItemStack, byte>(ref _items), 5);
            }
            if (flags[1])
            {
                length += span[length..].Read(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _items, 1)), 5);
            }

            if (flags[2])
            {
                length += span[length..].Read(ref Unsafe.As<ItemStack, byte>(ref _dyes), 5);
            }
            if (flags[3])
            {
                length += span[length..].Read(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _dyes, 1)), 5);
            }

            return length;
        }

        /// <inheritdoc/>
        protected override int WriteBody(Span<byte> span)
        {
            var flags = new ByteFlags();
            var length = 1;

            if (!Items[0].IsEmpty)
            {
                length += span[length..].Write(ref Unsafe.As<ItemStack, byte>(ref _items), 5);
                flags[0] = true;
            }
            if (!Items[1].IsEmpty)
            {
                length += span[length..].Write(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _items, 1)), 5);
                flags[1] = true;
            }

            if (!Dyes[0].IsEmpty)
            {
                length += span[length..].Write(ref Unsafe.As<ItemStack, byte>(ref _dyes), 5);
                flags[2] = true;
            }
            if (!Dyes[1].IsEmpty)
            {
                length += span[length..].Write(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _dyes, 1)), 5);
                flags[3] = true;
            }

            Unsafe.WriteUnaligned(ref span[0], flags);

            return length;
        }
    }
}
