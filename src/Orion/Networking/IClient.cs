using System;
using Orion.Networking.Packets;

namespace Orion.Networking {
    /// <summary>
    /// Provides a wrapper around a client.
    /// </summary>
    public interface IClient {
        /// <summary>
        /// Gets the client's index.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets a value indicating whether the client is connected.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets or sets the client's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        string Name { get; set; }

        /// <summary>
        /// Sends the given packet to the client.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <c>null</c>.</exception>
        void SendPacket(Packet packet);

        /// <summary>
        /// Sends the given packet to the client.
        /// </summary>
        /// <param name="packetType">The packet type.</param>
        /// <param name="text">The packet-specific text.</param>
        /// <param name="number">The first packet-specific number.</param>
        /// <param name="number2">The second packet-specific number.</param>
        /// <param name="number3">The third packet-specific number.</param>
        /// <param name="number4">The fourth packet-specific number.</param>
        /// <param name="number5">The fifth packet-specific number.</param>
        /// <param name="number6">The sixth packet-specific number.</param>
        /// <param name="number7">The seventh packet-specific number.</param>
        void SendPacket(PacketType packetType, string text = "", int number = 0, float number2 = 0, float number3 = 0,
                        float number4 = 0, int number5 = 0, int number6 = 0, int number7 = 0);
    }
}
