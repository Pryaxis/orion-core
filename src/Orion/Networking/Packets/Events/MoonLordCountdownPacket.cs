using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set the Moon Lord countdown.
    /// </summary>
    public sealed class MoonLordCountdownPacket : Packet {
        /// <summary>
        /// Gets or sets the Moon Lord countdown.
        /// </summary>
        public int MoonLordCountdown { get; set; }

        private protected override PacketType Type => PacketType.MoonLordCountdown;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            MoonLordCountdown = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(MoonLordCountdown);
        }
    }
}
