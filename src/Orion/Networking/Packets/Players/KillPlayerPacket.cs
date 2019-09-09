using System.IO;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to kill a player.
    /// </summary>
    public sealed class KillPlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the reason for the player's death.
        /// </summary>
        public Terraria.DataStructures.PlayerDeathReason PlayerDeathReason { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        public int HitDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the death is from PvP.
        /// </summary>
        public bool IsDeathFromPvp { get; set; }

        private protected override PacketType Type => PacketType.KillPlayer;

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerDeathReason = reader.ReadPlayerDeathReason();
            Damage = reader.ReadInt16();
            HitDirection = reader.ReadByte() - 1;
            IsDeathFromPvp = reader.ReadBoolean();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerDeathReason);
            writer.Write(Damage);
            writer.Write((byte)(HitDirection + 1));
            writer.Write(IsDeathFromPvp);
        }
    }
}
