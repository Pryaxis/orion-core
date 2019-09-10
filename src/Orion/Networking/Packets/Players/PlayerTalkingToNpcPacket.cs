using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set the NPC that a player is talking to.
    /// </summary>
    public sealed class PlayerTalkingToNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC index that the player is talking to.
        /// </summary>
        public short PlayerTalkingToNpcIndex { get; set; }

        private protected override PacketType Type => PacketType.PlayerTalkingToNpc;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} to N={PlayerTalkingToNpcIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerTalkingToNpcIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerTalkingToNpcIndex);
        }
    }
}
