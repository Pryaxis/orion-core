// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to unlock an object (chest, door, etc.).
    /// </summary>
    public sealed class UnlockObjectPacket : Packet {
        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        public UnlockObjectType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        public short ObjectX { get; set; }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        public short ObjectY { get; set; }

        public override PacketType Type => PacketType.UnlockObject;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ObjectType} @ ({ObjectX}, {ObjectY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ObjectType = (UnlockObjectType)reader.ReadByte();
            ObjectX = reader.ReadInt16();
            ObjectY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)ObjectType);
            writer.Write(ObjectX);
            writer.Write(ObjectY);
        }
    }
}
