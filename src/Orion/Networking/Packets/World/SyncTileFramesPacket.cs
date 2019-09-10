using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to synchronize tile frames. This is sent following every
    /// <see cref="SectionPacket"/>.
    /// </summary>
    public sealed class SyncTileFramesPacket : Packet {
        /// <summary>
        /// Gets or sets the starting section's X coordinate.
        /// </summary>
        public short StartSectionX { get; set; }

        /// <summary>
        /// Gets or sets the starting section's Y coordinate.
        /// </summary>
        public short StartSectionY { get; set; }

        /// <summary>
        /// Gets or sets the ending section's X coordinate.
        /// </summary>
        public short EndSectionX { get; set; }

        /// <summary>
        /// Gets or sets the ending section's Y coordinate.
        /// </summary>
        public short EndSectionY { get; set; }

        private protected override PacketType Type => PacketType.SyncTileFrames;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            StartSectionX = reader.ReadInt16();
            StartSectionY = reader.ReadInt16();
            EndSectionX = reader.ReadInt16();
            EndSectionY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(StartSectionX);
            writer.Write(StartSectionY);
            writer.Write(EndSectionX);
            writer.Write(EndSectionY);
        }
    }
}
