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

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to modify a chest.
    /// </summary>
    public sealed class ModifyChestPacket : Packet {
        /// <summary>
        /// Gets or sets the modification type.
        /// </summary>
        public ChestModificationType ModificationType { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY { get; set; }

        /// <summary>
        /// Gets or sets the chest style.
        /// </summary>
        public short ChestStyle { get; set; }

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex { get; set; }

        public override PacketType Type => PacketType.ModifyChest;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ModificationType}, #={ChestIndex} @ ({ChestX}, {ChestY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ModificationType = (ChestModificationType)reader.ReadByte();
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
            ChestStyle = reader.ReadInt16();
            ChestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)ModificationType);
            writer.Write(ChestX);
            writer.Write(ChestY);
            writer.Write(ChestStyle);
            writer.Write(ChestIndex);
        }
    }
}
