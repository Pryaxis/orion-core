using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the server to the client to synchronize a player's chest.
    /// </summary>
    public sealed class SyncPlayerChestPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's chest index.
        /// </summary>
        public short PlayerChestIndex { get; set; }

        private protected override PacketType Type => PacketType.SyncPlayerChest;

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
