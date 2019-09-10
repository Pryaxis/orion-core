using System.IO;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Items {
    /// <summary>
    /// Packet sent to set instanced item information.
    /// </summary>
    public sealed class InstancedItemInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the item's position.
        /// </summary>
        public Vector2 ItemPosition { get; set; }

        /// <summary>
        /// Gets or sets the item's velocity.
        /// </summary>
        public Vector2 ItemVelocity { get; set; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item should be disowned.
        /// </summary>
        public bool ShouldDisownItem { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        private protected override PacketType Type => PacketType.InstancedItemInfo;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemIndex = reader.ReadInt16();
            ItemPosition = reader.ReadVector2();
            ItemVelocity = reader.ReadVector2();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ShouldDisownItem = reader.ReadBoolean();
            ItemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemIndex);
            writer.Write(ItemPosition);
            writer.Write(ItemVelocity);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write(ShouldDisownItem);
            writer.Write((short)ItemType);
        }
    }
}
