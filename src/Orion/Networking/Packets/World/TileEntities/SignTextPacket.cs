using System;
using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set a sign's text. This is sent in response to a <see cref="RequestSignPacket"/>.
    /// </summary>
    public sealed class SignTextPacket : Packet {
        private string _signText;

        /// <summary>
        /// Gets or sets the sign's index.
        /// </summary>
        public short SignIndex { get; set; }

        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        public short SignX { get; set; }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        public short SignY { get; set; }

        /// <summary>
        /// Gets or sets the sign's text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string SignText {
            get => _signText;
            set => _signText = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.SignText;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SignIndex = reader.ReadInt16();
            SignX = reader.ReadInt16();
            SignY = reader.ReadInt16();
            _signText = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SignIndex);
            writer.Write(SignX);
            writer.Write(SignY);
            writer.Write(SignText);
        }
    }
}
