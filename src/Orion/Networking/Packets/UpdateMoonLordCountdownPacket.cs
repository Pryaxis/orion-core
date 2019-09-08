using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update the Moon Lord countdown.
    /// </summary>
    public sealed class UpdateMoonLordCountdownPacket : Packet {
        /// <summary>
        /// Gets or sets the Moon Lord countdown.
        /// </summary>
        public int MoonLordCountdown { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            MoonLordCountdown = reader.ReadInt32();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(MoonLordCountdown);
    }
}
