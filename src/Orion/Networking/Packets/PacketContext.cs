namespace Orion.Networking.Packets {
    /// <summary>
    /// Specifies the context with which to process a packet.
    /// </summary>
    public enum PacketContext {
        /// <summary>
        /// Indicates that the packet should be processed as the server.
        /// </summary>
        Server,

        /// <summary>
        /// Indicates that the packet should be processed as the client.
        /// </summary>
        Client,
    }
}
