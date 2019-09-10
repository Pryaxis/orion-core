using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent from the server to the client to show a combat number.
    /// </summary>
    public sealed class ShowCombatNumberPacket : Packet {
        /// <summary>
        /// Gets or sets the text's position.
        /// </summary>
        public Vector2 TextPosition { get; set; }

        /// <summary>
        /// Gets or sets the text's color.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Gets or sets the text number.
        /// </summary>
        public int TextNumber { get; set; }

        private protected override PacketType Type => PacketType.ShowCombatNumber;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TextPosition = reader.ReadVector2();
            TextColor = reader.ReadColor();
            TextNumber = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TextPosition);
            writer.Write(TextColor);
            writer.Write(TextNumber);
        }
    }
}
