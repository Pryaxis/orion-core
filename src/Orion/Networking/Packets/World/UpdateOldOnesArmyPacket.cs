using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to update the Old One's Army event.
    /// </summary>
    public sealed class UpdateOldOnesArmyPacket : Packet {
        /// <summary>
        /// Gets or sets the time left between waves.
        /// </summary>
        public int TimeLeftBetweenWaves { get; set; }

        private protected override PacketType Type => PacketType.UpdateOldOnesArmy;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            TimeLeftBetweenWaves = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(TimeLeftBetweenWaves);
        }
    }
}
