using System.IO;
using Orion.Items;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents an item frame that is transmitted over the network.
    /// </summary>
    public sealed class NetworkItemFrame : NetworkTileEntity, IItemFrame {
        /// <inheritdoc />
        public ItemType ItemType { get; set; }

        /// <inheritdoc />
        public int ItemStackSize { get; set; }

        /// <inheritdoc />
        public ItemPrefix ItemPrefix { get; set; }

        private protected override TileEntityType Type => TileEntityType.ItemFrame;

        private protected override void ReadFromReaderImpl(BinaryReader reader) {
            ItemType = (ItemType)reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemStackSize = reader.ReadInt16();
        }

        private protected override void WriteToWriterImpl(BinaryWriter writer) {
            writer.Write((short)ItemType);
            writer.Write((byte)ItemPrefix);
            writer.Write((short)ItemStackSize);
        }
    }
}
