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
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to place an object.
    /// </summary>
    public sealed class PlaceObjectPacket : Packet {
        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        public short ObjectX { get; set; }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        public short ObjectY { get; set; }

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        public BlockType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the object style.
        /// </summary>
        public short ObjectStyle { get; set; }

        /// <summary>
        /// Gets or sets the object's random state.
        /// </summary>
        public sbyte ObjectRandomState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the object direction.
        /// </summary>
        public bool ObjectDirection { get; set; }

        public override PacketType Type => PacketType.PlaceObject;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ObjectType}_{ObjectStyle} @ ({ObjectX}, {ObjectY}), ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ObjectX = reader.ReadInt16();
            ObjectY = reader.ReadInt16();
            ObjectType = (BlockType)reader.ReadInt16();
            ObjectStyle = reader.ReadInt16();
            reader.ReadByte();
            ObjectRandomState = reader.ReadSByte();
            ObjectDirection = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ObjectX);
            writer.Write(ObjectY);
            writer.Write((short)ObjectType);
            writer.Write(ObjectStyle);
            writer.Write((byte)0);
            writer.Write(ObjectRandomState);
            writer.Write(ObjectDirection);
        }
    }
}
