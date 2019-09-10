using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to the client to set an emote bubble.
    /// </summary>
    public sealed class EmoteBubblePacket : Packet {
        /// <summary>
        /// Gets or sets the emote index.
        /// </summary>
        public int EmoteIndex { get; set; }

        /// <summary>
        /// Gets or sets the anchor type.
        /// </summary>
        public byte AnchorType { get; set; }

        /// <summary>
        /// Gets or sets the anchor index.
        /// </summary>
        public ushort AnchorIndex { get; set; }

        /// <summary>
        /// Gets or sets the lifetime.
        /// </summary>
        public byte Lifetime { get; set; }

        /// <summary>
        /// Gets or sets the emotion.
        /// </summary>
        // TODO: implement enum for this.
        public int Emotion { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        public ushort Metadata { get; set; }

        private protected override PacketType Type => PacketType.EmoteBubble;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={EmoteIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            EmoteIndex = reader.ReadInt32();
            AnchorType = reader.ReadByte();
            if (AnchorType == byte.MaxValue) {
                return;
            }

            AnchorIndex = reader.ReadUInt16();
            Lifetime = reader.ReadByte();
            Emotion = reader.ReadSByte();
            if (Emotion < 0) {
                Metadata = reader.ReadUInt16();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(EmoteIndex);
            writer.Write(AnchorType);
            if (AnchorType == byte.MaxValue) {
                return;
            }

            writer.Write(AnchorIndex);
            writer.Write(Lifetime);
            writer.Write((sbyte)Emotion);
            if (Emotion < 0) {
                writer.Write(Metadata);
            }
        }
    }
}
