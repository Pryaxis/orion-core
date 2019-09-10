using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to try a password. This is sent in response to a
    /// <see cref="RequestPasswordPacket"/>.
    /// </summary>
    public sealed class PasswordResponsePacket : Packet {
        private string _password = "";

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string Password {
            get => _password;
            set => _password = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.PasswordResponse;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.PasswordResponse)}[P={Password}]";

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            _password = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Password);
        }
    }
}
