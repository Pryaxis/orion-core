using System;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a Terraria.Sign instance.
    /// </summary>
    public interface ISign : ITileEntity {
        /// <summary>
        /// Gets or sets the sign's text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        string Text { get; set; }
    }
}
