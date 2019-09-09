using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set the traveling merchant's shop.
    /// </summary>
    public sealed class TravelingMerchantShopPacket : Packet {
        /// <summary>
        /// Gets the shop item types.
        /// </summary>
        public ItemType[] ShopItemTypes { get; } = new ItemType[Terraria.Chest.maxItems];

        private protected override PacketType Type => PacketType.TravelingMerchantShop;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            for (var i = 0; i < Terraria.Chest.maxItems; ++i) {
                ShopItemTypes[i] = (ItemType)reader.ReadInt16();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            foreach (var itemType in ShopItemTypes) {
                writer.Write((short)itemType);
            }
        }
    }
}
