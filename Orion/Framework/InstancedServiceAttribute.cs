using System;

namespace Orion.Framework {
    /// <summary>
    /// An attribute that can be applied to a class to indicate that it is an instanced service, not a static service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InstancedServiceAttribute : Attribute { }
}
