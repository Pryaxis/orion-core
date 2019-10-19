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
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Packets.Extensions;

namespace Orion.Packets.Items {
    /// <summary>
    /// Packet sent to set item information. This is sent for item drops, updates, and pickups.
    /// </summary>
    public sealed class ItemInfoPacket : Packet {
        private short _itemIndex;
        private Vector2 _position;
        private Vector2 _velocity;
        private short _stackSize;
        private ItemPrefix _itemPrefix;
        private bool _canBePickedUpImmediately;
        private ItemType _itemType;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ItemInfo;

        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        /// <value>The item index.</value>
        public short ItemIndex {
            get => _itemIndex;
            set {
                _itemIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's position. The components are pixels.
        /// </summary>
        /// <value>The item's position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's velocity. The components are pixels per tick.
        /// </summary>
        /// <value>The item's velocity.</value>
        public Vector2 Velocity {
            get => _velocity;
            set {
                _velocity = value;
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
        /// Gets or sets a value indicating whether the item can be picked up immediately.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the item can be picked up immediately; otherwise, <see langword="false"/>.
        /// </value>
        public bool CanBePickedUpImmediately {
            get => _canBePickedUpImmediately;
            set {
                _canBePickedUpImmediately = value;
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

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _itemIndex = reader.ReadInt16();
            _position = reader.ReadVector2();
            _velocity = reader.ReadVector2();
            _stackSize = reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _canBePickedUpImmediately = reader.ReadBoolean();
            _itemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_itemIndex);
            writer.Write(in _position);
            writer.Write(in _velocity);
            writer.Write(_stackSize);
            writer.Write((byte)_itemPrefix);
            writer.Write(_canBePickedUpImmediately);
            writer.Write((short)_itemType);
        }
    }
}
