using OTAPI.Core;

namespace Orion.Framework
{
    /// <summary>
    /// This extends the base entity used for all terrarian entities to include a name.
    /// </summary>
    public abstract class NamedEntity : IEntity
    {
        /// <summary>
        /// Name of the entity
        /// </summary>
        public abstract string Name { get; protected set; }
    }
}
