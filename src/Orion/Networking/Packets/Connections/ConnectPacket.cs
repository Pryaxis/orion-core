using System;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to initiate a connection.
    /// </summary>
    public sealed class ConnectPacket : Packet {
        private string _version = "";

        /// <summary>
        /// Gets or sets the version, which is of the form "Terraria###". The client's version must match the server's
        /// in order for the connection to proceed.
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
