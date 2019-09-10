using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set Old One's Army information.
    /// </summary>
    public sealed class OldOnesArmyInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the time between waves.
        /// </summary>
        public TimeSpan TimeBetweenWaves { get; set; }

        private protected override PacketType Type => PacketType.OldOnesArmyInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.OldOnesArmyInfo)}[T={TimeBetweenWaves}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TimeBetweenWaves = TimeSpan.FromSeconds(reader.ReadInt32() / 60.0);
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((int)(TimeBetweenWaves.TotalSeconds * 60.0));
        }
    }
}
