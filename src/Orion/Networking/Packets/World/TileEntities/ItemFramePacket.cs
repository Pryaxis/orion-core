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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Entities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to set an item frame.
    /// </summary>
    public sealed class ItemFramePacket : Packet {
        private short _itemFrameX;
        private short _itemFrameY;
        private ItemType _itemType;
        private ItemPrefix _itemPrefix;
        private short _itemStackSize;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ItemFrame;

        /// <summary>
        /// Gets or sets the item frame's X coordinate.
        /// </summary>
        public short ItemFrameX {
            get => _itemFrameX;
            set {
                _itemFrameX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item frame's Y coordinate.
        /// </summary>
        public short ItemFrameY {
            get => _itemFrameY;
            set {
                _itemFrameY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ItemPrefix ItemPrefix {
            get => _itemPrefix;
            set {
                _itemPrefix = value ?? throw new ArgumentNullException(nameof(value));
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

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[({ItemFrameX}, {ItemFrameY}) is " +
            $"{(ItemPrefix != ItemPrefix.None ? ItemPrefix + " " : "")}{ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemFrameX = reader.ReadInt16();
            ItemFrameY = reader.ReadInt16();
            ItemType = ItemType.FromId(reader.ReadInt16()) ?? throw new PacketException("Item type is invalid.");
            ItemPrefix = ItemPrefix.FromId(reader.ReadByte()) ?? throw new PacketException("Item prefix is invalid.");
            ItemStackSize = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemFrameX);
            writer.Write(ItemFrameY);
            writer.Write(ItemType.Id);
            writer.Write((byte)ItemPrefix.Id);
            writer.Write(ItemStackSize);
        }
    }
}
