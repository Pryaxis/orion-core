using System;
using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to provide information about a player's inventory slot.
    /// </summary>
    public sealed class UpdatePlayerInventorySlotPacket : TerrariaPacket {
        private protected override int HeaderlessLength => 7;

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.UpdatePlayerInventorySlot;

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        public byte PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the inventory slot.
        /// </summary>
        public byte InventorySlot { get; set; }

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

        /// <summary>
        /// Reads an <see cref="UpdatePlayerInventorySlotPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static UpdatePlayerInventorySlotPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new UpdatePlayerInventorySlotPacket {
                PlayerId = reader.ReadByte(),
                InventorySlot = reader.ReadByte(),
                ItemStackSize = reader.ReadInt16(),
                ItemPrefix = (ItemPrefix)reader.ReadByte(),
                ItemType = (ItemType)reader.ReadInt16(),
            };
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerId);
            writer.Write(InventorySlot);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write((short)ItemType);
        }
    }
}
