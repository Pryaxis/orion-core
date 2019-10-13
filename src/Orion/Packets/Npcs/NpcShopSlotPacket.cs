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

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Orion.Items;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's shop slot. This is currently not naturally sent.
    /// </summary>
    public sealed class NpcShopSlotPacket : Packet {
        private byte _npcShopSlotIndex;
        private ItemType _itemType;
        private short _itemStackSize;
        private ItemPrefix _itemPrefix;
        private int _itemValue;
        private bool _canBuyItemOnce;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcShopSlot;

        /// <summary>
        /// Gets or sets the NPC shop slot index.
        /// </summary>
        public byte NpcShopSlotIndex {
            get => _npcShopSlotIndex;
            set {
                _npcShopSlotIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
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
        public short ItemStackSize {
            get => _itemStackSize;
            set {
                _itemStackSize = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
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
        public int ItemValue {
            get => _itemValue;
            set {
                _itemValue = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item can be bought only once.
        /// </summary>
        public bool CanBuyItemOnlyOnce {
            get => _canBuyItemOnce;
            set {
                _canBuyItemOnce = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ItemType} @ {NpcShopSlotIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcShopSlotIndex = reader.ReadByte();
            _itemType = (ItemType)reader.ReadInt16();
            _itemStackSize = reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _itemValue = reader.ReadInt32();

            Terraria.BitsByte flags = reader.ReadByte();
            if (flags[0]) {
                _canBuyItemOnce = true;
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcShopSlotIndex);
            writer.Write((short)_itemType);
            writer.Write(_itemStackSize);
            writer.Write((byte)_itemPrefix);
            writer.Write(_itemValue);

            Terraria.BitsByte flags = 0;
            flags[0] = _canBuyItemOnce;
            writer.Write(flags);
        }
    }
}
