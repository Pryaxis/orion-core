using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to continue connection with a server password.
    /// </summary>
    public sealed class ContinueConnectionWithServerPasswordPacket : Packet {
        private string _serverPassword = "";

        /// <summary>
        /// Gets or sets the server password.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string ServerPassword {
            get => _serverPassword;
            set => _serverPassword = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            _serverPassword = reader.ReadString();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(ServerPassword);
    }
}
