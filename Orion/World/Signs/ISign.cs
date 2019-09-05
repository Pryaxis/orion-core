using System;

namespace Orion.World.Signs {
    /// <summary>
    /// Provides a wrapper around a Terraria.Sign instance.
    /// </summary>
    public interface ISign {
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        string Text { get; set; }
    }
}
