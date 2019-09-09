using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set Old One's Army information.
    /// </summary>
    public sealed class OldOnesArmyInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the time left between waves.
        /// </summary>
        public int TimeLeftBetweenWaves { get; set; }

        private protected override PacketType Type => PacketType.OldOnesArmyInfo;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            TimeLeftBetweenWaves = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(TimeLeftBetweenWaves);
        }
    }
}
