using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update another player's chest.
    /// </summary>
    public sealed class UpdateOtherPlayerChestPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's chest index.
        /// </summary>
        public short PlayerChestIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            PlayerChestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerChestIndex);
        }
    }
}
