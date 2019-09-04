using Orion.Items;

namespace Orion.World.Chests {
    /// <summary>
    /// Provides a wrapper around a Terraria.Chest.
    /// </summary>
    public interface IChest {
        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        //IItemArray Items { get; }
    }
}
