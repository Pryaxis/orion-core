using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to synchronize tile frames.
    /// </summary>
    public sealed class SyncTileFramesPacket : Packet {
        /// <summary>
        /// Gets or sets the starting section X coordinate.
        /// </summary>
        public short StartSectionX { get; set; }

        /// <summary>
        /// Gets or sets the starting section Y coordinate.
        /// </summary>
        public short StartSectionY { get; set; }

        /// <summary>
        /// Gets or sets the ending section X coordinate.
        /// </summary>
        public short EndSectionX { get; set; }

        /// <summary>
        /// Gets or sets the ending section Y coordinate.
        /// </summary>
        public short EndSectionY { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            StartSectionX = reader.ReadInt16();
            StartSectionY = reader.ReadInt16();
            EndSectionX = reader.ReadInt16();
            EndSectionY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(StartSectionX);
            writer.Write(StartSectionY);
            writer.Write(EndSectionX);
            writer.Write(EndSectionY);
        }
    }
}
