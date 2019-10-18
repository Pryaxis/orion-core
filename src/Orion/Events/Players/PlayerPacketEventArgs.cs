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

using System;
using Orion.Players;
using Orion.Utils;

namespace Orion.Events.Players {
    /// <summary>
    /// Provides data for player-related packet events.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet.</typeparam>
    public abstract class PlayerPacketEventArgs<TPacket> : PlayerEventArgs, ICancelable, IDirtiable
            where TPacket : class, IDirtiable {
        private protected readonly TPacket _packet;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        public bool IsDirty => _packet.IsDirty;

        private protected PlayerPacketEventArgs(IPlayer player, TPacket packet) : base(player) {
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <inheritdoc/>
        public void Clean() => _packet.Clean();
    }
}
