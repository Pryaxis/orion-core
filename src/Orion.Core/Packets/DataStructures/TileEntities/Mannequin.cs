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
    /// Represents a serializable mannequin.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public sealed class Mannequin : SerializableTileEntity
    {
        [FieldOffset(0)] private ItemStack _items;
        [FieldOffset(64)] private ItemStack _dyes;

        /// <inheritdoc/>
        public override TileEntityId Id => TileEntityId.Mannequin;

        /// <summary>
        /// Gets the mannequin's items.
        /// </summary>
        /// <value>The mannequin's items.</value>
        public Span<ItemStack> Items => MemoryMarshal.CreateSpan(ref _items, 8);

        /// <summary>
        /// Gets the mannequin's dyes.
        /// </summary>
        /// <value>The mannequin's dyes.</value>
        public Span<ItemStack> Dyes => MemoryMarshal.CreateSpan(ref _dyes, 8);

        /// <inheritdoc/>
        protected override int ReadBody(Span<byte> span)
        {
            var flags = Unsafe.ReadUnaligned<ByteFlags>(ref span[0]);
            var flags2 = Unsafe.ReadUnaligned<ByteFlags>(ref span[1]);
            var length = 2;

            for (var i = 0; i < 8; ++i)
            {
                if (flags[i])
                {
                    length += span[length..].Read(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _items, i)), 5);
                }
            }

            for (var i = 0; i < 8; ++i)
            {
                if (flags2[i])
                {
                    length += span[length..].Read(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _dyes, i)), 5);
                }
            }

            return length;
        }

        /// <inheritdoc/>
        protected override int WriteBody(Span<byte> span)
        {
            var flags = new ByteFlags();
            var flags2 = new ByteFlags();
            var length = 2;

            for (var i = 0; i < 8; ++i)
            {
                if (!Items[i].IsEmpty)
                {
                    length += span[length..].Write(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _items, i)), 5);
                    flags[i] = true;
                }
            }

            for (var i = 0; i < 8; ++i)
            {
                if (!Dyes[i].IsEmpty)
                {
                    length += span[length..].Write(ref Unsafe.As<ItemStack, byte>(ref Unsafe.Add(ref _dyes, i)), 5);
                    flags2[i] = true;
                }
            }

            Unsafe.WriteUnaligned(ref span[0], flags);
            Unsafe.WriteUnaligned(ref span[1], flags2);

            return length;
        }
    }
}
