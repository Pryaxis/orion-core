using System.Collections.Generic;

namespace Orion.World.Signs {
    /// <summary>
    /// Provides a mechanism for managing signs.
    /// </summary>
    public interface ISignService : IReadOnlyList<ISign>, IService { }
}
