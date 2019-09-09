using System.IO;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to play a harp note from a player.
    /// </summary>
    public sealed class PlayerHarpNotePacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's harp note.
        /// </summary>
        public float PlayerHarpNote { get; set; }

        private protected override PacketType Type => PacketType.PlayerHarpNote;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerHarpNote = reader.ReadSingle();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerHarpNote);
        }
    }
}
