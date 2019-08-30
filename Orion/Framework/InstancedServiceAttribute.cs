namespace Orion.Framework {
    using System;

    /// <summary>
    /// An attribute that can be applied to a class to indicate that it is an instanced service, not a static service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InstancedServiceAttribute : Attribute {
    }
}
