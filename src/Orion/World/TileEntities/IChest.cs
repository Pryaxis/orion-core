using System;
using Orion.Items;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a Terraria.Chest.
    /// </summary>
    public interface IChest : ITileEntity {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        string Name { get; set; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        IItemList Items { get; }
    }
}
