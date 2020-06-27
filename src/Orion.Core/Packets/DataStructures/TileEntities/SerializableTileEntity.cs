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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.World.TileEntities;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    /// <summary>
    /// Provides the base class for a serializable tile entity.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public abstract class SerializableTileEntity
    {
        private static readonly IDictionary<TileEntityId, Func<SerializableTileEntity>> _ctors =
            new Dictionary<TileEntityId, Func<SerializableTileEntity>>
            {
                [TileEntityId.TargetDummy] = () => new TargetDummy(),
                [TileEntityId.ItemFrame] = () => new ItemFrame(),
                [TileEntityId.Sensor] = () => new Sensor(),
                [TileEntityId.Mannequin] = () => new Mannequin(),
                [TileEntityId.WeaponRack] = () => new WeaponRack(),
                [TileEntityId.HatRack] = () => new HatRack(),
                [TileEntityId.Plate] = () => new Plate(),
                [TileEntityId.Pylon] = () => new Pylon()
            };

        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(4)] private byte _bytes2;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets the tile entity's ID.
        /// </summary>
        /// <value>The tile entity's ID.</value>
        public abstract TileEntityId Id { get; }

        /// <summary>
        /// Gets or sets the tile entity's index.
        /// </summary>
        /// <value>The tile entity's index.</value>
        [field: FieldOffset(0)] public int Index { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's X coordinate.
        /// </summary>
        /// <value>The tile entity's X coordinate.</value>
        [field: FieldOffset(4)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's Y coordinate.
        /// </summary>
        /// <value>The tile entity's Y coordinate.</value>
        [field: FieldOffset(6)] public short Y { get; set; }

        /// <summary>
        /// Writes the tile entity to the given <paramref name="span"/>. Returns the number of bytes written to the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="includeIndex">Whether to include the tile entity's index.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public int Write(Span<byte> span, bool includeIndex)
        {
            Debug.Assert(span.Length >= (includeIndex ? 9 : 5));

            // Write the tile entity header with no bounds checking since we need to perform bounds checking later
            // anyways.
            ref var header = ref MemoryMarshal.GetReference(span);

            Unsafe.WriteUnaligned(ref header, Id);
            if (includeIndex)
            {
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref header, 1), ref _bytes, 8);
                return 9 + WriteBody(span[9..]);
            }
            else
            {
                Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref header, 1), ref _bytes2, 4);
                return 5 + WriteBody(span[5..]);
            }
        }

        /// <summary>
        /// Reads the tile entity's body from the given <paramref name="span"/>, mutating this instance. Returns the
        /// number of bytes read from the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        protected abstract int ReadBody(Span<byte> span);

        /// <summary>
        /// Writes the tile entity's body to the given <paramref name="span"/>. Returns the number of bytes written to
        /// the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        protected abstract int WriteBody(Span<byte> span);

        /// <summary>
        /// Reads a serializable tile entity from the given <paramref name="span"/>. Returns the number of bytes read
        /// from the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="includeIndex">Whether to include the tile entity's index.</param>
        /// <param name="tileEntity">The resulting tile entity.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(Span<byte> span, bool includeIndex, out SerializableTileEntity tileEntity)
        {
            Debug.Assert(span.Length >= (includeIndex ? 9 : 5));

            // Read the tile entity header with no bounds checking since we need to perform bounds checking later
            // anyways.
            ref var header = ref MemoryMarshal.GetReference(span);

            var id = Unsafe.ReadUnaligned<TileEntityId>(ref header);
            if (includeIndex)
            {
                tileEntity = _ctors.TryGetValue(id, out var ctor) ? ctor() : new UnknownTileEntity(span.Length - 9, id);
                Unsafe.CopyBlockUnaligned(ref tileEntity._bytes, ref Unsafe.Add(ref header, 1), 8);
                return 9 + tileEntity.ReadBody(span[9..]);
            }
            else
            {
                tileEntity = _ctors.TryGetValue(id, out var ctor) ? ctor() : new UnknownTileEntity(span.Length - 5, id);
                Unsafe.CopyBlockUnaligned(ref tileEntity._bytes2, ref Unsafe.Add(ref header, 1), 4);
                return 5 + tileEntity.ReadBody(span[5..]);
            }
        }
    }
}
