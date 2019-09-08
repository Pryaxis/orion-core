namespace Orion.Players {
    /// <summary>
    /// Specifies a player's difficulty.
    /// </summary>
    public enum PlayerDifficulty : byte {
        /// <summary>
        /// Softcore difficulty.
        /// </summary>
        Softcore = 0,

        /// <summary>
        /// Mediumcore difficulty. All items are dropped on death.
        /// </summary>
        Mediumcore = 1,

        /// <summary>
        /// Hardcore difficulty. Death is permanent.
        /// </summary>
        Hardcore = 2,
    }
}
