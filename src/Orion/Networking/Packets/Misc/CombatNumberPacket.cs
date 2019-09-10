using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent from the server to the client to show a combat number.
    /// </summary>
    public sealed class CombatNumberPacket : Packet {
        /// <summary>
        /// Gets or sets the number's position.
        /// </summary>
        public Vector2 NumberPosition { get; set; }

        /// <summary>
        /// Gets or sets the number's color.
        /// </summary>
        public Color NumberColor { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public int Number { get; set; }

        private protected override PacketType Type => PacketType.CombatNumber;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Number} ({NumberColor}) @ {NumberPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NumberPosition = reader.ReadVector2();
            NumberColor = reader.ReadColor();
            Number = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NumberPosition);
            writer.Write(NumberColor);
            writer.Write(Number);
        }
    }
}
