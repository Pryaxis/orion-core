using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the server to the client to set the client's status.
    /// </summary>
    public sealed class ClientStatusPacket : Packet {
        private Terraria.Localization.NetworkText _statusText = Terraria.Localization.NetworkText.Empty;

        /// <summary>
        /// Gets or sets the status increase.
        /// </summary>
        public int StatusIncrease { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Terraria.Localization.NetworkText StatusText {
            get => _statusText;
            set => _statusText = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.ClientStatus;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.ClientStatus)}[I={StatusIncrease}, T={StatusText}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            StatusIncrease = reader.ReadInt32();
            _statusText = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(StatusIncrease);
            writer.Write(StatusText);
        }
    }
}
