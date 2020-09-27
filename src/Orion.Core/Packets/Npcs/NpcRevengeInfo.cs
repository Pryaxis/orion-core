// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to update an NPC's revenge information.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 37)]
    public struct NpcRevengeInfo : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the revenge marker's unique ID.
        /// </summary>
        [field: FieldOffset(0)] public int UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [field: FieldOffset(4)] public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the NPC's net ID.
        /// </summary>
        [field: FieldOffset(12)] public int NetId { get; set; }

        /// <summary>
        /// Gets or sets the NPC's health percentage.
        /// </summary>
        [field: FieldOffset(16)] public float HealthPercentage { get; set; }

        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        [field: FieldOffset(20)] public int NpcType { get; set; }

        /// <summary>
        /// Gets or sets the NPC's AI style.
        /// </summary>
        [field: FieldOffset(24)] public int AiStyle { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates how much coin value the NPC steals upon death.
        /// </summary>
        [field: FieldOffset(28)] public int CoinStealValue { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the base (coin) drop value.
        /// </summary>
        [field: FieldOffset(32)] public float BaseValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the NPC is spawned from a statue.
        /// </summary>
        [field: FieldOffset(36)] public bool SpawnedFromStatue { get; set; }

        PacketId IPacket.Id => PacketId.NpcRevengeInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 37);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 37);
    }
}
