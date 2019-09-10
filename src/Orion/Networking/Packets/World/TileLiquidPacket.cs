using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to update a tile's liquid.
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

        private protected override PacketType Type => PacketType.TileLiquid;

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
