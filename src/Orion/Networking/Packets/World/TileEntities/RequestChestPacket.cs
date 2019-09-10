using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to request a chest's contents.
    /// </summary>
    public sealed class RequestChestPacket : Packet {
        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY { get; set; }

        private protected override PacketType Type => PacketType.RequestChest;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({ChestX}, {ChestY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChestX);
            writer.Write(ChestY);
        }
    }
}
