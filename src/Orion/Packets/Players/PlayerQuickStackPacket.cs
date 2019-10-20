﻿// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Players;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the client to the server to quick stack an inventory slot into a nearby chest.
    /// </summary>
    /// <remarks>
    /// This packet is used to perform server-sided quick stacking because the client does not know about all of the
    /// chests in the world.
    /// </remarks>
    public sealed class PlayerQuickStackPacket : Packet {
        private byte _inventorySlot;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerQuickStack;

        /// <summary>
        /// Gets or sets the player's inventory slot.
        /// </summary>
        /// <value>This player's inventory slot.</value>
        /// <remarks>This value can range from <c>0</c> to <c>219</c>.</remarks>
        /// <seealso cref="IPlayerInventory"/>
        public byte InventorySlot {
            get => _inventorySlot;
            set {
                _inventorySlot = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _inventorySlot = reader.ReadByte();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_inventorySlot);
    }
}
