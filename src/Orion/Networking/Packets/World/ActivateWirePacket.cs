using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to activate wire at a specific position.
    /// </summary>
    public sealed class ActivateWirePacket : Packet {
        /// <summary>
        /// Gets or sets the wire's X coordinate.
        /// </summary>
        public short WireX { get; set; }

        /// <summary>
        /// Gets or sets the wire's Y coordinate.
        /// </summary>
        public short WireY { get; set; }

        private protected override PacketType Type => PacketType.ActivateWire;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.ActivateWire)}[X={WireX}, Y={WireY}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            WireX = reader.ReadInt16();
            WireY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(WireX);
            writer.Write(WireY);
        }
    }
}
