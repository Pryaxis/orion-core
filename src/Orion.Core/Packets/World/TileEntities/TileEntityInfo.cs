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
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures.TileEntities;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World.TileEntities
{
    /// <summary>
    /// A packet sent from the server to the client to set a tile entity's information.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct TileEntityInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(4)] private bool _isActive;
        [FieldOffset(8)] private SerializableTileEntity? _tileEntity;

        /// <summary>
        /// Gets or sets the tile entity's index.
        /// </summary>
        /// <value>The tile entity's index.</value>
        [field: FieldOffset(0)] public int TileEntityIndex { get; set; }

        /// <summary>
        /// Gets or sets the tile entity. A value of <see langword="null"/> indicates that the tile entity is being
        /// removed.
        /// </summary>
        /// <value>The tile entity.</value>
        public SerializableTileEntity? TileEntity
        {
            get => _tileEntity;
            set => _tileEntity = value;
        }

        PacketId IPacket.Id => PacketId.TileEntityInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 5);
            if (!_isActive)
            {
                return length;
            }

            length += SerializableTileEntity.Read(span[length..], false, out _tileEntity);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            _isActive = _tileEntity != null;

            var length = span.Write(ref _bytes, 5);
            if (!_isActive)
            {
                return length;
            }

            length += _tileEntity!.Write(span[length..], false);
            return length;
        }
    }
}
