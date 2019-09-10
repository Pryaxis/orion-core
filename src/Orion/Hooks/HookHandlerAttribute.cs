using System;

namespace Orion.Hooks {
    /// <summary>
    /// An attribute that can be applied to a hook handler to indicate its priority.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HookHandlerAttribute : Attribute {
        /// <summary>
        /// Gets the priority of the handler.
        /// </summary>
        public HookPriority Priority { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HookHandlerAttribute"/> class with the specified priority.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public HookHandlerAttribute(HookPriority priority) {
            Priority = priority;
        }
    }
}
