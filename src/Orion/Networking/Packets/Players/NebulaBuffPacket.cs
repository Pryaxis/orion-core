using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Players;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to spread Nebula Armor buffs to nearby players.
    /// </summary>
    public sealed class NebulaBuffPacket : Packet {
        /// <summary>
        /// Gets or set the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff type.
        /// </summary>
        public BuffType BuffType { get; set; }

        /// <summary>
        /// Gets or sets the buff position.
        /// </summary>
        public Vector2 BuffPosition { get; set; }

        private protected override PacketType Type => PacketType.NebulaBuff;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {BuffType} at ({BuffPosition})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            BuffType = (BuffType)reader.ReadByte();
            BuffPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write((byte)BuffType);
            writer.Write(BuffPosition);
        }
    }
}
