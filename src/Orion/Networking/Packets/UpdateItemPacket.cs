using System.IO;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update an item in the world.
    /// </summary>
    public sealed class UpdateItemPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the item's position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the item's velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }

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
        public bool ShouldDisown { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ItemIndex = reader.ReadInt16();
            Position = reader.ReadVector2();
            Velocity = reader.ReadVector2();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ShouldDisown = reader.ReadBoolean();
            ItemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ItemIndex);
            writer.Write(Position);
            writer.Write(Velocity);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write(ShouldDisown);
            writer.Write((short)ItemType);
        }
    }
}
