using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a player's minion's target NPC.
    /// </summary>
    public sealed class UpdatePlayerMinionTargetNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's minion's target NPC index.
        /// </summary>
        public short PlayerMinionTargetNpcIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerMinionTargetNpcIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerMinionTargetNpcIndex);
        }
    }
}
