using System.Collections.Generic;

namespace Orion.Items {
    /// <summary>
    /// Provides a wrapper around an array of Terraria.Item instances that do not exist within the world.
    /// </summary>
    public interface IItemList : IReadOnlyList<IItem> { }
}
