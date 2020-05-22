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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's PvP status.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerPvpPacket : IPacket {
        [FieldOffset(0)] private byte _playerIndex;
        [FieldOffset(1)] private bool _isInPvp;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        public byte PlayerIndex {
            get => _playerIndex;
            set => _playerIndex = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in PvP.
        /// </summary>
        /// <value><see langword="true"/> if the player is in PvP; otherwise, <see langword="false"/>.</value>
        public bool IsInPvp {
            get => _isInPvp;
            set => _isInPvp = value;
        }

        PacketId IPacket.Id => PacketId.PlayerPvp;

        /// <inheritdoc/>
        public void Read(ReadOnlySpan<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(0), ref Unsafe.AsRef(in span[0]), 2);
        }

        /// <inheritdoc/>
        public void Write(ref Span<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref span[0], ref this.AsRefByte(0), 2);
            span = span[2..];
        }
    }
}
