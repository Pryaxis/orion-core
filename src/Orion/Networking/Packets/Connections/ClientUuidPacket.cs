using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to inform the server about the client's UUID. This is sent in response
    /// to a <see cref="ContinueConnectingPacket"/>.
    /// </summary>
    public sealed class ClientUuidPacket : Packet {
        private string _clientUuid;

        /// <summary>
        /// Gets or sets the client's UUID.
        /// </summary>
        public string ClientUuid {
            get => _clientUuid;
            set => _clientUuid = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.ClientUuid;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.ClientUuid)}[U={ClientUuid}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _clientUuid = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ClientUuid);
        }
    }
}
