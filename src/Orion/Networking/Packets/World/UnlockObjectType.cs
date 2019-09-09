namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Specifies the object type in an <see cref="UnlockObjectPacket"/>.
    /// </summary>
    public enum UnlockObjectType : byte {
        /// <summary>
        /// A chest.
        /// </summary>
        Chest = 1,

        /// <summary>
        /// A door.
        /// </summary>
        Door = 2,
    }
}
