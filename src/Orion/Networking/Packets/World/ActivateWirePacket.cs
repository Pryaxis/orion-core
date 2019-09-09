using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to activate a wire.
    /// </summary>
    public sealed class ActivateWirePacket : Packet {
        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short WireX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short WireY { get; set; }

        private protected override PacketType Type => PacketType.ActivateWire;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            WireX = reader.ReadInt16();
            WireY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(WireX);
            writer.Write(WireY);
        }
    }
}
