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

namespace Orion.Packets.Items {
    /// <summary>
    /// Packet sent from the server to the client to remove the owner of an item. This is sent if an item is 'claimed'
    /// but not picked up for too long.
    /// </summary>
    public sealed class ItemRemoveOwnerPacket : Packet {
        private short _itemIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ItemRemoveOwner;

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

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _itemIndex = reader.ReadInt16();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_itemIndex);
    }
}
