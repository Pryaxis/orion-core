using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to play a harp note.
    /// </summary>
    public sealed class PlayHarpNotePacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the harp note.
        /// </summary>
        public float HarpNote { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            HarpNote = reader.ReadSingle();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(HarpNote);
        }
    }
}
