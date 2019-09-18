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

using System;
using System.IO;
using Orion.Entities;
using Orion.Networking.Packets;
using Orion.World.TileEntities;

namespace Orion.Networking {
    /// <summary>
    /// Represents an item frame that is transmitted over the network.
    /// </summary>
    public sealed class NetworkItemFrame : NetworkTileEntity, IItemFrame {
        private ItemType _itemType;
        private int _itemStackSize;
        private ItemPrefix _itemPrefix;

        /// <inheritdoc />
        public override TileEntityType Type => TileEntityType.ItemFrame;

        /// <inheritdoc />
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public int ItemStackSize {
            get => _itemStackSize;
            set {
                _itemStackSize = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public ItemPrefix ItemPrefix {
            get => _itemPrefix;
            set {
                _itemPrefix = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        private protected override void ReadFromReaderImpl(BinaryReader reader) {
            ItemType = ItemType.FromId(reader.ReadInt16()) ?? throw new PacketException("Item type is invalid.");
            ItemPrefix = ItemPrefix.FromId(reader.ReadByte()) ?? throw new PacketException("Item prefix is invalid.");
            ItemStackSize = reader.ReadInt16();
        }

        private protected override void WriteToWriterImpl(BinaryWriter writer) {
            writer.Write(ItemType.Id);
            writer.Write((byte)ItemPrefix.Id);
            writer.Write((short)ItemStackSize);
        }
    }
}
