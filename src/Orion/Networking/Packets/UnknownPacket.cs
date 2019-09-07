using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Used as a fail-safe for any packet that failed to be read.
    /// </summary>
    public sealed class UnknownPacket : TerrariaPacket {
        private byte[] _payload = new byte[0];

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type { get; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public byte[] Payload {
            get => _payload;
            set => _payload = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownPacket"/> class with the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public UnknownPacket(TerrariaPacketType type) {
            Type = type;
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            _payload = reader.ReadBytes(packetLength);
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Payload);
        }
    }
}
