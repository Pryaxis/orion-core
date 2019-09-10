using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Players;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to add a buff to a player.
    /// </summary>
    public sealed class BuffPlayer : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the buff.
        /// </summary>
        public Buff Buff { get; set; }

        private protected override PacketType Type => PacketType.BuffPlayer;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {Buff}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            Buff = new Buff((BuffType)reader.ReadByte(), TimeSpan.FromSeconds(reader.ReadInt32() / 60.0));
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write((byte)Buff.BuffType);
            writer.Write((int)(Buff.Duration.TotalSeconds * 60.0));
        }
    }
}
