namespace Orion.World.Signs {
    /// <summary>
    /// Provides a wrapper around a Terraria.Sign instance.
    /// </summary>
    public interface ISign {
        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        string Text { get; set; }
    }
}
