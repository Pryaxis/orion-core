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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to request or set a chest's name.
    /// </summary>
    public sealed class ChestNamePacket : Packet {
        private string _chestName = "";

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY { get; set; }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        public string ChestName {
            get => _chestName;
            set => _chestName = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal override PacketType Type => PacketType.ChestName;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ChestIndex} @ ({ChestX}, {ChestY}) is {ChestName}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChestIndex = reader.ReadInt16();
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
            ChestName = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChestIndex);
            writer.Write(ChestX);
            writer.Write(ChestY);
            writer.Write(ChestName);
        }
    }
}
