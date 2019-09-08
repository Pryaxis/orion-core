using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update the Old One's Army invasion.
    /// </summary>
    public sealed class UpdateOldOnesArmyInvasionPacket : Packet {
        /// <summary>
        /// Gets or sets the time left between waves.
        /// </summary>
        public int TimeLeftBetweenWaves { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength)
            => TimeLeftBetweenWaves = reader.ReadInt32();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(TimeLeftBetweenWaves);
    }
}
