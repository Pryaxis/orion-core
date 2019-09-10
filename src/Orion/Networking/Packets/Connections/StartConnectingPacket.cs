using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to initiate a connection.
    /// </summary>
    public sealed class StartConnectingPacket : Packet {
        private string _version = "";

        /// <summary>
        /// Gets or sets the version. This is usually of the form <c>"Terraria###"</c>.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string Version {
            get => _version;
            set => _version = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.StartConnecting;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.StartConnecting)}[V={Version}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _version = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(Version);
        }
    }
}
