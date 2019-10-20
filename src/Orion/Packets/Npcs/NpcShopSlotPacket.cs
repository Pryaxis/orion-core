// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using System.IO;
using Orion.Items;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's shop slot.
    /// </summary>
    /// <remarks>This packet is not normally sent. It can be used to provide custom NPC shops.</remarks>
    public sealed class NpcShopSlotPacket : Packet {
        private byte _shopSlot;
        private ItemType _itemType;
        private short _stackSize;
        private ItemPrefix _itemPrefix;
        private int _value;
        private bool _canBuyOnlyOnce;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcShopSlot;

        /// <summary>
        /// Gets or sets shop slot.
        /// </summary>
        /// <value>The shop slot.</value>
        /// <remarks>This property's value can range from <c>0</c> to <c>39</c>.</remarks>
        public byte ShopSlot {
            get => _shopSlot;
            set {
                _shopSlot = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
        /// <value>The item's type.</value>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        public short StackSize {
            get => _stackSize;
            set {
                _stackSize = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <value>The item's prefix.</value>
        public ItemPrefix ItemPrefix {
            get => _itemPrefix;
            set {
                _itemPrefix = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's value.
        /// </summary>
        /// <value>The item's value.</value>
        /// <remarks>
        /// This property's value corresponds to copper coins 1:1. Thus, a value of <c>100</c> corresponds to a silver
        /// coin, a value of <c>10000</c> corresponds to a gold coin, and a value of <c>1000000</c> corresponds to a
        /// platinum coin.
        /// </remarks>
        public int Value {
            get => _value;
            set {
                _value = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item can be bought only once.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the item can be bought only once; otherwise, <see langword="false"/>.
        /// </value>
        public bool CanBuyOnlyOnce {
            get => _canBuyOnlyOnce;
            set {
                _canBuyOnlyOnce = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _shopSlot = reader.ReadByte();
            _itemType = (ItemType)reader.ReadInt16();
            _stackSize = reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _value = reader.ReadInt32();

            Terraria.BitsByte flags = reader.ReadByte();
            _canBuyOnlyOnce = flags[0];
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_shopSlot);
            writer.Write((short)_itemType);
            writer.Write(_stackSize);
            writer.Write((byte)_itemPrefix);
            writer.Write(_value);

            Terraria.BitsByte flags = 0;
            flags[0] = _canBuyOnlyOnce;
            writer.Write(flags);
        }
    }
}
