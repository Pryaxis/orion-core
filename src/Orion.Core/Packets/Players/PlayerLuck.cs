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
    /// A packet sent to set a player's luck.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct PlayerLuck : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's ladybug luck duration, in ticks. A negative value results in bad luck.
        /// </summary>
        /// <value>The player's ladybug luck duration, in ticks.</value>
        [field: FieldOffset(1)] public int LadybugTicks { get; set; }

        /// <summary>
        /// Gets or sets the player's torch luck. A negative value results in bad luck.
        /// </summary>
        /// <value>The player's torch luck.</value>
        [field: FieldOffset(5)] public float Torch { get; set; }

        /// <summary>
        /// Gets or sets the player's luck potion strength. This ranges from <c>0</c> to <c>3</c>.
        /// </summary>
        /// <value>The player's luck potion strength.</value>
        [field: FieldOffset(9)] public byte PotionStrength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a garden gnome.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a garden gnome; otherwise, <see langword="false"/>.
        /// </value>
        [field: FieldOffset(10)] public bool IsNearGardenGnome { get; set; }

        PacketId IPacket.Id => PacketId.PlayerLuck;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 11);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 11);
    }
}
