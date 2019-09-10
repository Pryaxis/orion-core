namespace Orion.Players {
    /// <summary>
    /// Specifies a player's difficulty.
    /// </summary>
    public enum PlayerDifficulty : byte {
        /// <summary>
        /// Indicates softcore difficulty.
        /// </summary>
        Softcore = 0,

        /// <summary>
        /// Indicates mediumcore difficulty. All items are dropped on death.
        /// </summary>
        Mediumcore = 1,

        /// <summary>
        /// Indicates hardcore difficulty. Death is permanent.
        /// </summary>
        Hardcore = 2,
    }
}
