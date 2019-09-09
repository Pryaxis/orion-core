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

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ChestX);
            writer.Write(ChestY);
        }
    }
}
