namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Specifies the toggle type in a <see cref="ToggleDoorPacket"/>.
    /// </summary>
    public enum ToggleDoorAction : byte {
        /// <summary>
        /// Opening a door.
        /// </summary>
        OpenDoor = 0,

        /// <summary>
        /// Closing a door.
        /// </summary>
        CloseDoor,

        /// <summary>
        /// Opening a trapdoor.
        /// </summary>
        OpenTrapdoor,

        /// <summary>
        /// Closing a trapdoor.
        /// </summary>
        CloseTrapdoor,

        /// <summary>
        /// Opening a tall gate.
        /// </summary>
        OpenTallGate,

        /// <summary>
        /// Closing a tall gate.
        /// </summary>
        CloseTallGate,
    }
}
