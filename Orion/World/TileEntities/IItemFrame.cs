using Orion.Items;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a Terraria item frame tile entity.
    /// </summary>
    public interface IItemFrame : ITileEntity {
        /// <summary>
        /// Gets the framed item.
        /// </summary>
        IItem Item { get; }
    }
}
