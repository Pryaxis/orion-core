using System;

namespace Orion {
    /// <summary>
    /// An attribute that can be applied to a service implementation class to indicate that it is an instanced service,
    /// and not a static service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InstancedServiceAttribute : Attribute { }
}
