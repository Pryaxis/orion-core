using System.Collections.Generic;

namespace Orion.World.Chests {
    /// <summary>
    /// Provides a mechanism for managing chests.
    /// </summary>
    public interface IChestService : IReadOnlyList<IChest>, IService { }
}
