using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Players;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to cause nearby players to gain Nebula Armor buffs.
    /// </summary>
    public sealed class LevelUpNebulaArmorPacket : Packet {
        /// <summary>
        /// Gets or set the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff type.
        /// </summary>
        public BuffType BuffType { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            BuffType = (BuffType)reader.ReadByte();
            Position = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write((byte)BuffType);
            writer.Write(Position);
        }
    }
}
