namespace Orion.Networking.Packets {
    /// <summary>
    /// Indicates the type of a <see cref="TerrariaPacket"/>.
    /// </summary>
    public enum TerrariaPacketType {
        /// <summary>
        /// Indicates a <see cref="ConnectionRequestPacket"/>.
        /// </summary>
        ConnectionRequest = 1,

        /// <summary>
        /// Indicates a <see cref="DisconnectPacket"/>.
        /// </summary>
        Disconnect = 2,

        /// <summary>
        /// Indicates a <see cref="ContinueConnectingPacket"/>.
        /// </summary>
        ContinueConnecting = 3,
    }
}
