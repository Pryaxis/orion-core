using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update the traveling merchant's shop.
    /// </summary>
    public sealed class UpdateTravelingMerchantInventoryPacket : Packet {
        /// <summary>
        /// Gets the shop items.
        /// </summary>
        public ItemType[] ShopItems { get; } = new ItemType[Terraria.Chest.maxItems];

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            for (var i = 0; i < Terraria.Chest.maxItems; ++i) {
                ShopItems[i] = (ItemType)reader.ReadInt16();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            foreach (var itemType in ShopItems) {
                writer.Write((short)itemType);
            }
        }
    }
}
