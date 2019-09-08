using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to request a sign.
    /// </summary>
    public sealed class RequestSignPacket : Packet {
        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        public short SignX { get; set; }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        public short SignY { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            SignX = reader.ReadInt16();
            SignY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(SignX);
            writer.Write(SignY);
        }
    }
}
