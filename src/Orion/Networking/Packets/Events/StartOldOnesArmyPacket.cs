using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the client to the server to start the Old One's Army event.
    /// </summary>
    public sealed class StartOldOnesArmyPacket : Packet {
        /// <summary>
        /// Gets or sets the crystal's X coordinate.
        /// </summary>
        public short CrystalX { get; set; }

        /// <summary>
        /// Gets or sets the crystal's Y coordinate.
        /// </summary>
        public short CrystalY { get; set; }

        private protected override PacketType Type => PacketType.StartOldOnesArmy;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.StartOldOnesArmy)}[X={CrystalX}, Y={CrystalY}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            CrystalX = reader.ReadInt16();
            CrystalY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(CrystalX);
            writer.Write(CrystalY);
        }
    }
}
