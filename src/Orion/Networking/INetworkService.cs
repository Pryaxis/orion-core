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
using Orion.Hooks;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using Orion.Utils;

namespace Orion.Networking {
    /// <summary>
    /// Represents a service that manages the network. Provides network-related hooks and methods.
    /// </summary>
    public interface INetworkService : IReadOnlyArray<IClient>, IService {
        /// <summary>
        /// Gets or sets the hook handlers that occur when a packet was received.
        /// </summary>
        HookHandlerCollection<ReceivedPacketEventArgs> ReceivedPacket { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a packet is being received. This hook can be handled.
        /// </summary>
        HookHandlerCollection<ReceivingPacketEventArgs> ReceivingPacket { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a packet was sent.
        /// </summary>
        HookHandlerCollection<SentPacketEventArgs> SentPacket { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a packet is being sent. This hook can be handled.
        /// </summary>
        HookHandlerCollection<SendingPacketEventArgs> SendingPacket { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a client is disconnected.
        /// </summary>
        HookHandlerCollection<ClientDisconnectedEventArgs> ClientDisconnected { get; set; }

        /// <summary>
        /// Broadcasts the given packet to everyone except the excluded index.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="excludeIndex">The exclude index. <c>-1</c> represents no-one.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <c>null</c>.</exception>
        void BroadcastPacket(Packet packet, int excludeIndex = -1);

        /// <summary>
        /// Broadcasts the given packet to everyone except the excluded index.
        /// </summary>
        /// <param name="packetType">The packet type.</param>
        /// <param name="excludeIndex">The exclude index. <c>-1</c> represents no-one.</param>
        /// <param name="text">The text.</param>
        /// <param name="number">The first packet-specific number.</param>
        /// <param name="number2">The second packet-specific number.</param>
        /// <param name="number3">The third packet-specific number.</param>
        /// <param name="number4">The fourth packet-specific number.</param>
        /// <param name="number5">The fifth packet-specific number.</param>
        /// <param name="number6">The sixth packet-specific number.</param>
        /// <param name="number7">The seventh packet-specific number.</param>
        void BroadcastPacket(PacketType packetType, int excludeIndex = -1, string text = "", int number = 0,
                             float number2 = 0, float number3 = 0, float number4 = 0, int number5 = 0, int number6 = 0,
                             int number7 = 0);
    }
}
