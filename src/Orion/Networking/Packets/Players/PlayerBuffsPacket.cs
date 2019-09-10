using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's buffs. Each buff will be set for one second.
    /// </summary>
    public sealed class PlayerBuffsPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets the player's buff types.
        /// </summary>
        public BuffType[] PlayerBuffTypes { get; } = new BuffType[Terraria.Player.maxBuffs];

        private protected override PacketType Type => PacketType.PlayerBuffs;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            for (var i = 0; i < PlayerBuffTypes.Length; ++i) {
                PlayerBuffTypes[i] = (BuffType)reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            foreach (var buffType in PlayerBuffTypes) {
                writer.Write((byte)buffType);
            }
        }
    }
}
