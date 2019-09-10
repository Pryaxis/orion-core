using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the server to the client to allow it to continue connecting. This is sent in response to either
    /// a <see cref="StartConnectingPacket"/> or a valid <see cref="PasswordResponsePacket"/>.
    /// </summary>
    public sealed class ContinueConnectingPacket : Packet {
        /// <summary>
        /// Gets or sets the player index that the client will begin using.
        /// </summary>
        public byte PlayerIndex { get; set; }

        private protected override PacketType Type => PacketType.ContinueConnecting;
        
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.ContinueConnecting)}[P={PlayerIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
        }
    }
}
