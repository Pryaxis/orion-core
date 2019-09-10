namespace Orion.World {
    /// <summary>
    /// Specifies the type of an invasion.
    /// </summary>
    public enum InvasionType : sbyte {
        /// <summary>
        /// Indicates no invasion.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates the Goblins.
        /// </summary>
        Goblins = 1,

        /// <summary>
        /// Indicates the Frost Legion.
        /// </summary>
        FrostLegion = 2,

        /// <summary>
        /// Indicates the Pirates.
        /// </summary>
        Pirates = 3,

        /// <summary>
        /// Indicates the Martians.
        /// </summary>
        Martians = 4,
    }
}
