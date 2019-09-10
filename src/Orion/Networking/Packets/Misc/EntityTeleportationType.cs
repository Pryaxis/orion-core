namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Specifies the teleportation type of an <see cref="EntityTeleportationPacket"/>.
    /// </summary>
    public enum EntityTeleportationType {
        /// <summary>
        /// Teleport a player.
        /// </summary>
        TeleportPlayer = 0,

        /// <summary>
        /// Teleport an NPC.
        /// </summary>
        TeleportNpc = 1,

        /// <summary>
        /// Teleport a player to another player (via a Wormhole Potion).
        /// </summary>
        TeleportPlayerToOtherPlayer = 2,
    }
}
