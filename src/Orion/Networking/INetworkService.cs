using System;
using System.Collections.Generic;
using Orion.Hooks;
using Orion.Networking.Events;
using Orion.Networking.Packets;

namespace Orion.Networking {
    /// <summary>
    /// Provides a mechanism for managing the network.
    /// </summary>
    public interface INetworkService : IReadOnlyList<IClient>, IService {
        /// <summary>
        /// Occurs when a packet was received.
        /// </summary>
        HookHandlerCollection<ReceivedPacketEventArgs> ReceivedPacket { get; set; }

        /// <summary>
        /// Occurs when a packet is being received. This event can be handled.
        /// </summary>
        HookHandlerCollection<ReceivingPacketEventArgs> ReceivingPacket { get; set; }

        /// <summary>
        /// Occurs when a packet was sent.
        /// </summary>
        HookHandlerCollection<SentPacketEventArgs> SentPacket { get; set; }

        /// <summary>
        /// Occurs when a packet is being sent. This event can be handled.
        /// </summary>
        HookHandlerCollection<SendingPacketEventArgs> SendingPacket { get; set; }

        /// <summary>
        /// Occurs when a client is disconnected.
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
