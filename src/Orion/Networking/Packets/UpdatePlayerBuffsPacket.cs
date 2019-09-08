using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's buffs.
    /// </summary>
    public sealed class UpdatePlayerBuffsPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets the player buffs.
        /// </summary>
        public BuffType[] PlayerBuffs { get; } = new BuffType[Terraria.Player.maxBuffs];

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            for (var i = 0; i < PlayerBuffs.Length; ++i) {
                PlayerBuffs[i] = (BuffType)reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            foreach (var buffType in PlayerBuffs) {
                writer.Write((byte)buffType);
            }
        }
    }
}
