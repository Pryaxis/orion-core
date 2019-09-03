using System;
using Orion.Framework;
using Orion.Networking.Events;
using Orion.Networking.Packets;

namespace Orion.Networking {
    /// <summary>
    /// Provides a mechanism for managing the network. All implementations must be thread-safe.
    /// </summary>
    public interface INetworkService : IService {
        /// <summary>
        /// Occurs when a packet was received.
        /// </summary>
        event EventHandler<ReceivedPacketEventArgs> ReceivedPacket;

        /// <summary>
        /// Occurs when a packet is being received. This event can be handled.
        /// </summary>
        event EventHandler<ReceivingPacketEventArgs> ReceivingPacket;

        /// <summary>
        /// Occurs when a packet was sent.
        /// </summary>
        event EventHandler<SentPacketEventArgs> SentPacket;

        /// <summary>
        /// Occurs when a packet is being sent. This event can be handled.
        /// </summary>
        event EventHandler<SendingPacketEventArgs> SendingPacket;

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
        void SendPacket(
            TerrariaPacketType packetType, int targetIndex = -1, int exceptIndex = -1, string text = "",
            int number = default, float number2 = default, float number3 = default, float number4 = default,
            int number5 = default, int number6 = default, int number7 = default);
    }
}
