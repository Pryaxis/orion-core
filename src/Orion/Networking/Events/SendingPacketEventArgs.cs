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
using System.ComponentModel;
using Orion.Networking.Packets;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.SendingPacket"/> handlers.
    /// </summary>
    public sealed class SendingPacketEventArgs : HandledEventArgs {
        private Packet _packet;

        /// <summary>
        /// Gets the packet's receiver.
        /// </summary>
        public IClient Receiver { get; }

        /// <summary>
        /// Gets or sets the packet that is being sent.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Packet Packet {
            get => _packet;
            set {
                _packet = value ?? throw new ArgumentNullException(nameof(value));
                MarkPacketAsDirty();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the packet is dirty.
        /// </summary>
        public bool IsPacketDirty { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendingPacketEventArgs"/> class with the specified receiver
        /// and packet.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="receiver"/> or <paramref name="packet"/> are <c>null</c>.
        /// </exception>
        public SendingPacketEventArgs(IClient receiver, Packet packet) {
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <summary>
        /// Marks the packet as being dirty.
        /// </summary>
        public void MarkPacketAsDirty() => IsPacketDirty = true;
    }
}
