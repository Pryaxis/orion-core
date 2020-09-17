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

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set an item animation.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 7)]
    public struct PlayerAnimation : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the item rotation.
        /// </summary>
        [field: FieldOffset(1)] public float ItemRotation { get; set; }

        /// <summary>
        /// Gets or sets the animation time (in frames).
        /// </summary>
        [field: FieldOffset(5)] public short AnimationTime { get; set; }

        PacketId IPacket.Id => PacketId.PlayerAnimation;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 7);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 7);
    }
}
