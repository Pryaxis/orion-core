using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to provide information about a player's inventory slot.
    /// </summary>
    public sealed class UpdatePlayerInventorySlotPacket : TerrariaPacket {
        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.UpdatePlayerInventorySlot;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the inventory slot index.
        /// </summary>
        public byte InventorySlotIndex { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            InventorySlotIndex = reader.ReadByte();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(InventorySlotIndex);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write((short)ItemType);
        }
    }
}
