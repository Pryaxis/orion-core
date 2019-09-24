// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using JetBrains.Annotations;
using Orion.Packets;
using Orion.Players;

namespace Orion.Events.Packets {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PacketSend"/> event.
    /// </summary>
    [PublicAPI]
    public sealed class PacketSendEventArgs : PacketEventArgs {
        /// <summary>
        /// Gets the receiver.
        /// </summary>
        [NotNull]
        public IPlayer Receiver { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketSendEventArgs"/> class with the given receiver and
        /// packet.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="receiver"/> or <paramref name="packet"/> are <c>null</c>.
        /// </exception>
        public PacketSendEventArgs([NotNull] IPlayer receiver, [NotNull] Packet packet) : base(packet) {
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        }
    }
}
