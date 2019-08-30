namespace Orion.Framework {
    using System;

    /// <summary>
    /// An attribute that can be applied to a class to indicate that it overrides a service.
    /// </summary>
    /// <remarks>
    /// Having multiple implementations of a service with this attribute results in undefined behavior.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class OverrideServiceAttribute : Attribute {
    }
}
