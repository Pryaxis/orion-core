// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Orion.Entities;
using Orion.Utils;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set the traveling merchant's shop.
    /// </summary>
    [PublicAPI]
    public sealed class TravelingMerchantShopPacket : Packet {
        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || ShopItemTypes.IsDirty;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TravelingMerchantShop;

        /// <summary>
        /// Gets the shop's <see cref="ItemType"/>s.
        /// </summary>
        [NotNull]
        public DirtiableArray<ItemType> ShopItemTypes { get; } = new DirtiableArray<ItemType>(Terraria.Chest.maxItems);

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[...]";

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            ShopItemTypes.Clean();
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            for (var i = 0; i < ShopItemTypes.Count; ++i) {
                ShopItemTypes._array[i] = (ItemType)reader.ReadInt16();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            foreach (var itemType in ShopItemTypes) {
                writer.Write((short)itemType);
            }
        }
    }
}
