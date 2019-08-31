namespace Orion.Networking.Packets {
    using System;
    using System.IO;

    /// <summary>
    /// Used as a fail-safe for any packet that failed to be read.
    /// </summary>
    public sealed class UnknownPacket : TerrariaPacket {
        private byte[] _bytes = new byte[0];

        /// <summary>
        /// Gets or sets the byte array.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public byte[] Bytes {
            get => _bytes;
            set => _bytes = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc />
        private protected override int HeaderlessLength => Bytes.Length;

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownPacket"/> class with the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public UnknownPacket(TerrariaPacketType type) {
            Type = type;
        }

        /// <summary>
        /// Reads an <see cref="UnknownPacket"/> from the given reader with the specified type and length.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        /// <param name="headerlessLength">The length.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="headerlessLength"/> is too big.</exception>
        public static UnknownPacket FromReader(BinaryReader reader, TerrariaPacketType type, ushort headerlessLength) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            if (HeaderLength + headerlessLength > ushort.MaxValue) {
                throw new ArgumentOutOfRangeException(nameof(headerlessLength), "Length is too long.");
            }

            var bytes = reader.ReadBytes(headerlessLength);
            return new UnknownPacket(type) {_bytes = bytes};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Bytes);
        }
    }
}
