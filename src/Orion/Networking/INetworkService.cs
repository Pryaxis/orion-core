using System;
using Orion.Hooks;
using Orion.Networking.Events;
using Orion.Networking.Packets;

namespace Orion.Networking {
    /// <summary>
    /// Provides a mechanism for managing the network.
    /// </summary>
    public interface INetworkService : IService {
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
        /// Sends the given packet to the target index, excepting the exception index.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="targetIndex">The target index. <c>-1</c> represents everyone.</param>
        /// <param name="exceptIndex">The except index. <c>-1</c> represents no-one.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="targetIndex"/> is out of range.</exception>
        void SendPacket(Packet packet, int targetIndex = -1, int exceptIndex = -1);

        /// <summary>
        /// Sends the specified packet to the target index excepting the given except index.
        /// </summary>
        /// <param name="packetType">The packet type.</param>
        /// <param name="targetIndex">The target index. <c>-1</c> represents everyone.</param>
        /// <param name="exceptIndex">The except index. <c>-1</c> represents no-one.</param>
        /// <param name="text">The text.</param>
        /// <param name="number">The first packet-specific number.</param>
        /// <param name="number2">The second packet-specific number.</param>
        /// <param name="number3">The third packet-specific number.</param>
        /// <param name="number4">The fourth packet-specific number.</param>
        /// <param name="number5">The fifth packet-specific number.</param>
        /// <param name="number6">The sixth packet-specific number.</param>
        /// <param name="number7">The seventh packet-specific number.</param>
        void SendPacket(PacketType packetType, int targetIndex = -1, int exceptIndex = -1, string text = "",
                        int number = 0, float number2 = 0, float number3 = 0, float number4 = 0, int number5 = 0,
                        int number6 = 0, int number7 = 0);
    }
}
