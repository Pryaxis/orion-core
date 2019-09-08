using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to start an Old One's Army event.
    /// </summary>
    public sealed class StartOldOnesArmyInvasionPacket : Packet {
        /// <summary>
        /// Gets or sets the crystal's X coordinate.
        /// </summary>
        public short CrystalX { get; set; }

        /// <summary>
        /// Gets or sets the crystal's Y coordinate.
        /// </summary>
        public short CrystalY { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            CrystalX = reader.ReadInt16();
            CrystalY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(CrystalX);
            writer.Write(CrystalY);
        }
    }
}
