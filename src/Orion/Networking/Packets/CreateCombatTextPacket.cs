using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to create combat text.
    /// </summary>
    public sealed class CreateCombatTextPacket : Packet {
        /// <summary>
        /// Gets or sets the text's X position.
        /// </summary>
        public float TextX { get; set; }

        /// <summary>
        /// Gets or sets the text's Y position.
        /// </summary>
        public float TextY { get; set; }

        /// <summary>
        /// Gets or sets the text's color.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Gets or sets the text number.
        /// </summary>
        public int TextNumber { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            TextX = reader.ReadSingle();
            TextY = reader.ReadSingle();
            TextColor = reader.ReadColor();
            TextNumber = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(TextX);
            writer.Write(TextY);
            writer.Write(TextColor);
            writer.Write(TextNumber);
        }
    }
}
