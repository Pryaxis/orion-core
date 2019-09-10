using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the server to the client to synchronize a player's chest index.
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

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, C={PlayerChestIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerChestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerChestIndex);
        }
    }
}
