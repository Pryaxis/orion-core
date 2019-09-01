using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Used as a fail-safe for any packet that failed to be read.
    /// </summary>
    public sealed class UnknownPacket : TerrariaPacket {
        private byte[] _bytes = new byte[0];

        /// <inheritdoc />
        private protected override int HeaderlessLength => Bytes.Length;

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type { get; }

        /// <summary>
        /// Gets or sets the byte array.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public byte[] Bytes {
            get => _bytes;
            set => _bytes = value ?? throw new ArgumentNullException(nameof(value));
        }

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

            return new UnknownPacket(type) {_bytes = reader.ReadBytes(headerlessLength)};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Bytes);
        }
    }
}
