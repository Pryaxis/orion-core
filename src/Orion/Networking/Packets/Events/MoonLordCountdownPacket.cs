using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set the Moon Lord countdown.
    /// </summary>
    public sealed class MoonLordCountdownPacket : Packet {
        /// <summary>
        /// Gets or sets the Moon Lord countdown.
        /// </summary>
        public TimeSpan MoonLordCountdown { get; set; }

        private protected override PacketType Type => PacketType.MoonLordCountdown;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.MoonLordCountdown)}[T={MoonLordCountdown}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            MoonLordCountdown = TimeSpan.FromSeconds(reader.ReadInt32() / 60.0);
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((int)(MoonLordCountdown.TotalSeconds * 60.0));
        }
    }
}
