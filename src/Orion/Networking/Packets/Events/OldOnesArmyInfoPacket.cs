using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set Old One's Army information.
    /// </summary>
    public sealed class OldOnesArmyInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the time left between waves.
        /// </summary>
        public int TimeLeftBetweenWaves { get; set; }

        private protected override PacketType Type => PacketType.OldOnesArmyInfo;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TimeLeftBetweenWaves = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TimeLeftBetweenWaves);
        }
    }
}
