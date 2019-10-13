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

using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to activate wire at a specific position.
    /// </summary>
    public sealed class WireActivatePacket : Packet {
        private short _wireY;
        private short _wireX;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.WireActivate;

        /// <summary>
        /// Gets or sets the wire's X coordinate.
        /// </summary>
        public short WireX {
            get => _wireX;
            set {
                _wireX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the wire's Y coordinate.
        /// </summary>
        public short WireY {
            get => _wireY;
            set {
                _wireY = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({WireX}, {WireY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _wireX = reader.ReadInt16();
            _wireY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_wireX);
            writer.Write(_wireY);
        }
    }
}
