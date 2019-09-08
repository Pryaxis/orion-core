using Orion.Items;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a Terraria item frame tile entity.
    /// </summary>
    public interface IItemFrame : ITileEntity {
        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        int ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item prefix.
        /// </summary>
        ItemPrefix ItemPrefix { get; set; }
    }
}
