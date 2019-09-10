using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to move an inventory slot into a chest.
    /// </summary>
    public sealed class MoveIntoChestPacket : Packet {
        /// <summary>
        /// Gets or sets the player inventory slot index.
        /// </summary>
        public byte PlayerInventorySlotIndex { get; set; }

        private protected override PacketType Type => PacketType.MoveIntoChest;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerInventorySlotIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerInventorySlotIndex);
        }
    }
}
