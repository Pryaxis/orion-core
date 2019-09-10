using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to perform a tile modification.
    /// </summary>
    public sealed class TileModificationPacket : Packet {
        /// <summary>
        /// Gets or sets the modification type.
        /// </summary>
        public TileModificationType ModificationType { get; set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the modification data.
        /// </summary>
        public short ModificationData { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public byte ModificationStyle { get; set; }

        private protected override PacketType Type => PacketType.TileModification;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() {
            switch (ModificationType) {
            case TileModificationType.DestroyBlock:
                return $"{Type}[{ModificationType} @ ({TileX}, {TileY}), {ModificationData == 1}]";
            case TileModificationType.PlaceBlock:
                return $"{Type}[{ModificationType}, {(BlockType)ModificationData}_{ModificationStyle} @ ({TileX}, {TileY})]";
            case TileModificationType.DestroyWall:
                return $"{Type}[{ModificationType} @ ({TileX}, {TileY}), {ModificationData == 1}]";
            case TileModificationType.PlaceWall:
                return $"{Type}[{ModificationType}, {(WallType)ModificationData} @ ({TileX}, {TileY})]";
            case TileModificationType.SlopeBlock:
                return $"{Type}[{ModificationType}, {(SlopeType)ModificationData} @ ({TileX}, {TileY})]";
            default:
                return $"{Type}[{ModificationType} @ ({TileX}, {TileY})]";
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ModificationType = (TileModificationType)reader.ReadByte();
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            ModificationData = reader.ReadInt16();
            ModificationStyle = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)ModificationType);
            writer.Write(TileX);
            writer.Write(TileY);
            writer.Write(ModificationData);
            writer.Write(ModificationStyle);
        }
    }
}
