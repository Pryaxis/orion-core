using System.IO;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to damage a player.
    /// </summary>
    public sealed class DamagePlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the reason for the player's (potential) death.
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
        /// Gets or sets the hit cooldown.
        /// </summary>
        public int HitCooldown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsHitCritical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is from PvP.
        /// </summary>
        public bool IsHitFromPvp { get; set; }

        private protected override PacketType Type => PacketType.DamagePlayer;

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerDeathReason = reader.ReadPlayerDeathReason();
            Damage = reader.ReadInt16();
            HitDirection = reader.ReadByte() - 1;
            Terraria.BitsByte flags = reader.ReadByte();
            IsHitCritical = flags[0];
            IsHitFromPvp = flags[1];
            HitCooldown = reader.ReadSByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerDeathReason);
            writer.Write(Damage);
            writer.Write((byte)(HitDirection + 1));
            Terraria.BitsByte flags = 0;
            flags[0] = IsHitCritical;
            flags[1] = IsHitFromPvp;
            writer.Write(flags);
            writer.Write((sbyte)HitCooldown);
        }
    }
}
