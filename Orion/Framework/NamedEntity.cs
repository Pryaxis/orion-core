using OTAPI.Core;

namespace Orion.Framework
{
    /// <summary>
    /// TODO: this mean i have no intentions of ever coming back to do this
    /// </summary>
    public abstract class NamedEntity : IEntity
    {
        public abstract string Name { get; protected set; }
    }
}
