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

using System.IO;
using Orion.Items;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent from the client to the server to perform a mass wire operation.
    /// </summary>
    /// <remarks>This packet is sent when a player uses <see cref="ItemType.TheGrandDesign"/>.</remarks>
    /// <seealso cref="WireOperations"/>
    public sealed class MassWireOperationPacket : Packet {
        private short _startX;
        private short _startY;
        private short _endX;
        private short _endY;
        private WireOperations _operations;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.MassWireOperation;

        /// <summary>
        /// Gets or sets the starting tile's X coordinate.
        /// </summary>
        /// <value>The starting tile's X coordinate.</value>
        public short StartX {
            get => _startX;
            set {
                _startX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the starting tile's Y coordinate.
        /// </summary>
        /// <value>The starting tile's Y coordinate.</value>
        public short StartY {
            get => _startY;
            set {
                _startY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ending tile's X coordinate.
        /// </summary>
        /// <value>The ending tile's X coordinate.</value>
        public short EndX {
            get => _endX;
            set {
                _endX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the ending tile's Y coordinate.
        /// </summary>
        /// <value>The ending tile's Y coordinate.</value>
        public short EndY {
            get => _endY;
            set {
                _endY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the wire operations.
        /// </summary>
        /// <value>The wire operations.</value>
        public WireOperations Operations {
            get => _operations;
            set {
                _operations = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _startX = reader.ReadInt16();
            _startY = reader.ReadInt16();
            _endX = reader.ReadInt16();
            _endY = reader.ReadInt16();
            _operations = (WireOperations)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_startX);
            writer.Write(_startY);
            writer.Write(_endX);
            writer.Write(_endY);
            writer.Write((byte)_operations);
        }
    }
}
