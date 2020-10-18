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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to display an NPC emote.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 10)]
    public struct NpcEmote : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the emote bubble ID.
        /// </summary>
        [field: FieldOffset(0)] public int EmoteBubbleId { get; set; }

        /// <summary>
        /// Gets or sets the world UI anchor type.
        /// </summary>
        [field: FieldOffset(4)] public WorldUiAnchorType UiAnchorType { get; set; }

        /// <summary>
        /// Gets or sets the anchor entity's index.
        /// </summary>
        [field: FieldOffset(5)] public ushort AnchorEntityIndex { get; set; }

        /// <summary>
        /// Gets or sets the emote's lifetime.
        /// </summary>
        [field: FieldOffset(7)] public ushort LifeTime { get; set; }

        /// <summary>
        /// Gets or sets the emote ID.
        /// </summary>
        [field: FieldOffset(9)] public byte EmoteId { get; set; }

        PacketId IPacket.Id => PacketId.NpcEmote;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 10);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 10);
    }

    /// <summary>
    /// Specifies a user interface anchor.
    /// </summary>
    public enum WorldUiAnchorType : byte
    {
        /// <summary>
        /// Indicates an NPC.
        /// </summary>
        Npc,

        /// <summary>
        /// Indicates a player.
        /// </summary>
        Player,

        /// <summary>
        /// Indicates a projectile.
        /// </summary>
        Projectile
    }
}
