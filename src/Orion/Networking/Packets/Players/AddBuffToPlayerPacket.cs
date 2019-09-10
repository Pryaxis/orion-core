using System;
using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to add a buff to a player.
    /// </summary>
    public sealed class AddBuffToPlayerPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff. The buff duration is limited to approximately 546.1 seconds.
        /// </summary>
        public Buff Buff { get; set; }

        private protected override PacketType Type => PacketType.AddBuffToPlayer;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            Buff = new Buff((BuffType)reader.ReadByte(), TimeSpan.FromSeconds(reader.ReadInt32() / 60.0));
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write((byte)Buff.BuffType);
            writer.Write((int)(Buff.Duration.TotalSeconds * 60.0));
        }
    }
}
