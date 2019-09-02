using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to request connection.
    /// </summary>
    public sealed class RequestConnectionPacket : TerrariaPacket {
        private string _version = "";

        /// <inheritdoc />
        public override bool IsSentToClient => false;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.RequestConnection;

        /// <summary>
        /// Gets or sets the version, which is of the form $"Terraria{Main.curRelease}".
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
