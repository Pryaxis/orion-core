namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Represents an invasion type that is transmitted over the network.
    /// </summary>
    public enum NetworkInvasionType : short {
        /// <summary>
        /// The Goblins.
        /// </summary>
        Goblins = -1,

        /// <summary>
        /// The Frost Legion.
        /// </summary>
        FrostLegion = -2,

        /// <summary>
        /// The Pirates.
        /// </summary>
        Pirates = -3,

        /// <summary>
        /// A pumpkin moon.
        /// </summary>
        PumpkinMoon = -4,

        /// <summary>
        /// A frost moon.
        /// </summary>
        FrostMoon = -5,

        /// <summary>
        /// A solar eclipse.
        /// </summary>
        Eclipse = -6,

        /// <summary>
        /// The Martians.
        /// </summary>
        Martians = -7,

        /// <summary>
        /// The Moon Lord.
        /// </summary>
        MoonLord = -8,
    }
}
