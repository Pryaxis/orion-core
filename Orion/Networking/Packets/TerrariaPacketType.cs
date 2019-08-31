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
        /// Indicates a <see cref="ContinueConnectionPacket"/>.
        /// </summary>
        ContinueConnecting = 3,

        /// <summary>
        /// Indicates a <see cref="PlayerInfoPacket"/>.
        /// </summary>
        PlayerInfo = 4,
        
        /// <summary>
        /// Indicates a <see cref="PlayerInventorySlotPacket"/>.
        /// </summary>
        PlayerInventorySlot = 5,
        
        /// <summary>
        /// Indicates a <see cref="FinishConnectionPacket"/>.
        /// </summary>
        FinishConnection = 6,
        
        /// <summary>
        /// Indicates a <see cref="WorldInfoPacket"/>.
        /// </summary>
        WorldInfo = 7,
    }
}
