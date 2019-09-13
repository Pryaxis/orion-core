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
    /// Packet sent to set a tile's liquid.
    /// </summary>
    public sealed class TileLiquidPacket : Packet {
        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the liquid amount.
        /// </summary>
        public byte LiquidAmount { get; set; }

        /// <summary>
        /// Gets or sets the liquid type.
        /// </summary>
        public LiquidType LiquidType { get; set; }

        public override PacketType Type => PacketType.TileLiquid;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{LiquidAmount} x{LiquidType} @ ({TileX}, {TileY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            LiquidAmount = reader.ReadByte();
            LiquidType = (LiquidType)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TileX);
            writer.Write(TileY);
            writer.Write(LiquidAmount);
            writer.Write((byte)LiquidType);
        }
    }
}
