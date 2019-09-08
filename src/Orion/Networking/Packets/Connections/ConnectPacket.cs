using System;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to initiate a connection. The client's version must match the server's
    /// version in order to proceed.
    /// </summary>
    public sealed class ConnectPacket : Packet {
        private string _version = "";

        private protected override PacketType Type => PacketType.Connect;

        /// <summary>
        /// Gets or sets the version. This is usually of the form <c>"Terraria###"</c>.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string Version {
            get => _version;
            set => _version = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            _version = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Version);
        }
    }
}
