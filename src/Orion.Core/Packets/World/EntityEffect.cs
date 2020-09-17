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

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to play sounds, spawn skeletrons, start sundialing or create mimic smoke.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct EntityEffect : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        [field: FieldOffset(1)] public EntityEffectAction Action { get; set; }

        PacketId IPacket.Id => PacketId.EntityEffect;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 2);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 2);
    }

    /// <summary>
    /// Specifies an <see cref="EntityEffect"/> action.
    /// </summary>
    public enum EntityEffectAction : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        SpawnSkeletron = 1,
        PlaySound = 2,
        StartSundialing = 3,
        BigMimicSpawnSmoke = 4
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
